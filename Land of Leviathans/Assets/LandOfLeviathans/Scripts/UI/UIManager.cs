using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SA.UI;

namespace SA
{
    public class UIManager : MonoBehaviour
    {
        public float lerpSpeed = 2;
        public Slider health;
        public Slider h_vis;
        public Slider focus;
        public Slider f_vis;
        public Slider stamina;
        public Slider s_vis;
        public Text souls;
        public float sizeMultiplier = 2;
        int curSouls;

        public static UIManager singleton;
        public GesturesManager gestures;


        public GameObject interactCard;
        public Text ac_action_type;

        int ac_index;
        public List<AnnounceCard> ann_cards;

        void Start()
        {
            gestures = GesturesManager.singleton;
            interactCard.SetActive(false);
            CloseCards();
            CloseAnnounceType();
        }

        void Awake()
        {
            singleton = this;
        }

        public void InitSouls(int v)
        {
            curSouls = v;
        }

        public void InitSlider(StatSlider t , int value)
        {
            Slider s = null;
            Slider v = null;

            switch (t)
            {
                case StatSlider.health:
                    s = health;
                    v = h_vis;
                    break;
                case StatSlider.focus:
                    s = focus;
                    v = f_vis;
                    break;
                case StatSlider.stamina:
                    s = stamina;
                    v = s_vis;
                    break;
                default:
                    break;
            }

            s.maxValue = value;
            v.maxValue = value;
            RectTransform r = s.GetComponent<RectTransform>();
            RectTransform r_v = v.GetComponent<RectTransform>();
            float value_actual = value * sizeMultiplier;
            value_actual = Mathf.Clamp(value_actual, 0, 1000);
            r.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, value_actual);
            r_v.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, value_actual);
        }

        public void Tick(CharacterStats stats, float delta)
        {
            GameUI(stats, delta);
        }

        void GameUI(CharacterStats stats, float delta)
        {
            health.value = Mathf.Lerp(health.value, stats._health, delta * lerpSpeed * 2);
            focus.value = Mathf.Lerp(focus.value, stats._focus, delta * lerpSpeed * 2);
            stamina.value = stats._stamina;

            curSouls = Mathf.RoundToInt(Mathf.Lerp(curSouls, stats._souls, delta * lerpSpeed));
            souls.text = curSouls.ToString();

            h_vis.value = Mathf.Lerp(h_vis.value, stats._health, delta * lerpSpeed);
            f_vis.value = Mathf.Lerp(f_vis.value, stats._focus, delta * lerpSpeed);
            s_vis.value = Mathf.Lerp(s_vis.value, stats._stamina, delta * lerpSpeed);
        }

        public void AffectAll(int h, int f, int s)
        {
            InitSlider(StatSlider.health, h);
            InitSlider(StatSlider.focus, f);
            InitSlider(StatSlider.stamina, s);
        }

        public enum StatSlider
        {
            health,focus,stamina
        }

        public void OpenAnnounceType(UIActionType t)
        {
            switch (t)
            {
                case UIActionType.pickup:
                    ac_action_type.text = StaticStrings.ui_ac_pick;
                    break;
                case UIActionType.interact:
                    break;
                case UIActionType.open:
                    ac_action_type.text = StaticStrings.ui_ac_open;
                    break;
                case UIActionType.talk:
                    ac_action_type.text = StaticStrings.ui_ac_talk;
                    break;
                default:
                    break;
            }

            interactCard.SetActive(true);
        }

        public void AddAnnounceCard(Item i)
        {
            ann_cards[ac_index].itemName.text = i.name_item;
            ann_cards[ac_index].icon.sprite = i.icon;
            ann_cards[ac_index].gameObject.SetActive(true);
            ac_index++;

            if(ac_index > 5)
            {
                ac_index = 0;
            }
        }

        public void CloseCards()
        {
            for (int i = 0; i < ann_cards.Count; i++)
            {
                ann_cards[i].gameObject.SetActive(false);
            }
        }

        public void CloseAnnounceType()
        {
            interactCard.SetActive(false);
        }

    }

    public enum UIActionType
    {
        pickup,interact,open,talk
    }
}
