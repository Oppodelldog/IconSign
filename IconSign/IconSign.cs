using Jotunn;
using Jotunn.Managers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace IconSign
{
    internal class IconSign : MonoBehaviour, Hoverable, Interactable, TextReceiver
    {
        internal const string TranslationKeyName = "$iconsign_name";
        internal const string TranslationKeyUse = "$iconsign_use";

        [FormerlySerializedAs("m_name")] public string mName;

        [FormerlySerializedAs("m_defaultText")]
        public string mDefaultText = "T_emote_thumbsup";

        private ZNetView _mNview;
        private bool _mIsViewable = true;
        private string _mCurrentText = "T_emote_thumbsup";

        private void Awake()
        {
            mName = Localization.instance.Localize(TranslationKeyName);
            var canvas = gameObject.GetComponentInChildren<Canvas>();
            var woodPole = gameObject.FindDeepChild("wood_pole (1)");
            var sign = gameObject.GetComponent<Sign>();
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

            GUIManager.Instance.CreateImage(
                text: GetText(),
                parent: canvas.transform,
                anchorMin: new Vector2(0.5f, 0.5f),
                anchorMax: new Vector2(0.5f, 0.5f),
                position: new Vector2(0, 0),
                size: new Vector2(0.35f, 0.35f)).GetComponent<RectTransform>().position += new Vector3(0.01f, 0, 0);

            _mNview = GetComponent<ZNetView>();
            if (_mNview.GetZDO() == null)
                return;
            UpdateText();
            InvokeRepeating(nameof(UpdateText), 2f, 2f);
        }

        public string GetHoverText()
        {
            var str = _mIsViewable ? "\"" + GetText().RemoveRichTextTags() + "\"" : "[TEXT HIDDEN DUE TO UGC SETTINGS]";
            return !PrivateArea.CheckAccess(transform.position, flash: false)
                ? str
                : str + "\n" + Localization.instance.Localize(mName + "\n[<color=yellow><b>$KEY_Use</b></color>] " + TranslationKeyUse);
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
                        _mIsViewable = true;
                        break;
                    case PrivilegeManager.Result.NotAllowed:
                        _mCurrentText = "";
                        UpdateSprite();
                        _mIsViewable = false;
                        break;
                    case PrivilegeManager.Result.Failed:
                    default:
                        _mCurrentText = "";
                        UpdateSprite();
                        _mIsViewable = false;
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

        public bool UseItem(Humanoid user, ItemDrop.ItemData item) => false;

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
                Jotunn.Logger.LogError($"{descriptiveName} is null");
            }
        }
    }
}