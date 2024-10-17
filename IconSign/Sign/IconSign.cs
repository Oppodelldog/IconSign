using IconSign.Config;
using IconSign.Data;
using IconSign.Extensions;
using IconSign.Selection;
using Jotunn;
using Jotunn.Managers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Logger = Jotunn.Logger;

namespace IconSign.Sign
{
    internal class IconSign : MonoBehaviour, Hoverable, Interactable, TextReceiver
    {
        [FormerlySerializedAs("m_name")] public string mName;

        [FormerlySerializedAs("m_defaultText")]
        public string mDefaultText = "rested";

        private ZNetView _mNview;
        private string _mCurrentText = "rested";

        private void Awake()
        {
            mName = LocalizationManager.Instance.TryTranslate(Constants.TranslationKeyName);
            var canvas = gameObject.GetComponentInChildren<Canvas>();
            var woodPole = gameObject.FindDeepChild("wood_pole (1)");
            var sign = gameObject.GetComponent<global::Sign>();
            var collider = gameObject.GetComponentInChildren<Collider>();
            LOGIfNull(canvas, "Canvas");
            LOGIfNull(woodPole, "WoodPole");
            LOGIfNull(sign, "Sign");
            LOGIfNull(collider, "Collider");

            var text = canvas.transform.GetChild(0);
            LOGIfNull(text, "Text");

            Destroy(text.gameObject);
            Destroy(sign);

            canvas.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0.5f, 0.5f);
            woodPole.transform.localScale = new Vector3(0.5f, 0.5f, 0.1f);
            collider.transform.localScale = new Vector3(0.5f, 1f, 1f);

            var imgObj = GUIManager.Instance.CreateImage(
                spriteName: GetText(),
                parent: canvas.transform,
                anchorMin: new Vector2(0.5f, 0.5f),
                anchorMax: new Vector2(0.5f, 0.5f),
                pivot: new Vector2(0.5f, 0.5f),
                position: new Vector2(0, 0),
                size: new Vector2(0.35f, 0.35f));

            imgObj.GetComponent<RectTransform>().localPosition += new Vector3(0.0f, 0, Constants.BlitPreventionOffset);
            _mNview = GetComponent<ZNetView>();
            if (_mNview.GetZDO() == null)
                return;
            UpdateText();
            InvokeRepeating(nameof(UpdateText), 2f, 2f);
        }

        public string GetHoverText()
        {
            var str = "";
            if (DevConfig.IconSign.ShowInternalName.Value) str += "\"" + GetText().RemoveRichTextTags() + "\"\n";
            if (!PrivateArea.CheckAccess(transform.position, flash: false)) return str;
            str += "\n" + mName
                        + Localization.instance.Localize("\n[<color=yellow><b>$KEY_Use</b></color>] ")
                        + LocalizationManager.Instance.TryTranslate(Constants.TranslationKeyUse);

            str += "\n[<color=yellow><b>1-8</b></color>] " + LocalizationManager.Instance.TryTranslate(Constants.TranslationKeyPaintItem);

            return str;
        }

        public string GetHoverName() => mName;

        public bool Interact(Humanoid character, bool hold, bool alt)
        {
            if (hold || !PrivateArea.CheckAccess(transform.position))
                return false;

            IconSelectionPanel.Instance.RequestSelection();
            IconSelectionPanel.Instance.OnIconSelected += OnIconSelected;
            return true;
        }

        private void OnIconSelected(string icon)
        {
            IconSelectionPanel.Instance.OnIconSelected -= OnIconSelected;
            if (icon == "") return;

            RecentIcons.Add(icon);
            SetText(icon);
        }

        private void UpdateText()
        {
            var text = _mNview.GetZDO().GetString(ZDOVars.s_text, mDefaultText);
            var str = _mNview.GetZDO().GetString(ZDOVars.s_author);
            text = CensorShittyWords.FilterUGC(text, UGCType.Text, str);
            if (_mCurrentText == text)
                return;
            PrivilegeManager.CanViewUserGeneratedContent(str, access =>
            {
                switch (access)
                {
                    case PrivilegeManager.Result.Allowed:
                        _mCurrentText = text;
                        UpdateSprite();
                        break;
                    case PrivilegeManager.Result.NotAllowed:
                        _mCurrentText = "";
                        UpdateSprite();
                        break;
                    case PrivilegeManager.Result.Failed:
                    default:
                        _mCurrentText = "";
                        UpdateSprite();
                        ZLog.LogError("Failed to check UGC privilege");
                        break;
                }
            });
        }

        private void UpdateSprite()
        {
            gameObject.GetComponentInChildren<Image>().sprite = GUIManager.Instance.GetSprite(_mCurrentText);
        }

        public string GetText() => _mCurrentText;

        public bool UseItem(Humanoid user, ItemDrop.ItemData item)
        {
            var iconName = item.GetIcon().name;
            RecentIcons.Add(iconName);
            SetText(iconName);

            return true;
        }

        public void SetText(string text)
        {
            if (!PrivateArea.CheckAccess(transform.position))
                return;
            _mNview.ClaimOwnership();
            _mNview.GetZDO().Set(ZDOVars.s_text, text);
            _mNview.GetZDO().Set(ZDOVars.s_author, PrivilegeManager.GetNetworkUserId());
            UpdateText();
        }

        private static void LOGIfNull(object obj, string descriptiveName)
        {
            if (obj == null)
            {
                Logger.LogError($"{descriptiveName} is null");
            }
        }
    }
}