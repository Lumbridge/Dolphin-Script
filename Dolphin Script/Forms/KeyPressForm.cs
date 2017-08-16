using System;
using System.Windows.Forms;

using static DolphinScript.Lib.Backend.GlobalVariables;

using DolphinScript.Lib.ScriptEventClasses;

namespace DolphinScript
{
    public partial class KeyPressForm : Form
    {
        private MainForm MainFormHandle;

        public KeyPressForm(MainForm mf)
        {
            InitializeComponent();

            MainFormHandle = mf;

            CenterToParent();
        }

        private void Button_AddKeypressEvent_Click(object sender, EventArgs e)
        {
            AllEvents.Add(new KeyboardKeyPress { KeyboardKeys = KeyPressEventTextBox.Text });

            MainFormHandle.UpdateListBox(MainFormHandle);
        }

        private void KeyPressForm_Load(object sender, EventArgs e)
        {
            //SpecialKeysComboBox.Items.Add(KEY_SHIFT);
            //SpecialKeysComboBox.Items.Add(KEY_ALT);
            SpecialKeysComboBox.Items.Add(KEY_LEFT_ARROW);
            SpecialKeysComboBox.Items.Add(KEY_RIGHT_ARROW);
            SpecialKeysComboBox.Items.Add(KEY_UP_ARROW);
            SpecialKeysComboBox.Items.Add(KEY_DOWN_ARROW);
            SpecialKeysComboBox.Items.Add(KEY_BACKSPACE);
            SpecialKeysComboBox.Items.Add(KEY_BREAK);
            SpecialKeysComboBox.Items.Add(KEY_CAPS_LOCK);
            SpecialKeysComboBox.Items.Add(KEY_DELETE);
            SpecialKeysComboBox.Items.Add(KEY_END);
            SpecialKeysComboBox.Items.Add(KEY_ENTER);
            SpecialKeysComboBox.Items.Add(KEY_ESC);
            SpecialKeysComboBox.Items.Add(KEY_HELP);
            SpecialKeysComboBox.Items.Add(KEY_HOME);
            SpecialKeysComboBox.Items.Add(KEY_INSERT);
            SpecialKeysComboBox.Items.Add(KEY_NUM_LOCK);
            SpecialKeysComboBox.Items.Add(KEY_PAGE_DOWN);
            SpecialKeysComboBox.Items.Add(KEY_PAGE_UP);
            SpecialKeysComboBox.Items.Add(KEY_PRINT_SCREEN);
            SpecialKeysComboBox.Items.Add(KEY_SCROLL_LOCK);
            SpecialKeysComboBox.Items.Add(KEY_TAB);
            SpecialKeysComboBox.Items.Add(KEY_F1);
            SpecialKeysComboBox.Items.Add(KEY_F2);
            SpecialKeysComboBox.Items.Add(KEY_F3);
            SpecialKeysComboBox.Items.Add(KEY_F4);
            SpecialKeysComboBox.Items.Add(KEY_F5);
            SpecialKeysComboBox.Items.Add(KEY_F6);
            SpecialKeysComboBox.Items.Add(KEY_F7);
            SpecialKeysComboBox.Items.Add(KEY_F8);
            SpecialKeysComboBox.Items.Add(KEY_F9);
            SpecialKeysComboBox.Items.Add(KEY_F10);
            SpecialKeysComboBox.Items.Add(KEY_F11);
            SpecialKeysComboBox.Items.Add(KEY_F12);
            SpecialKeysComboBox.Items.Add(KEY_F13);
            SpecialKeysComboBox.Items.Add(KEY_F14);
            SpecialKeysComboBox.Items.Add(KEY_F15);
            SpecialKeysComboBox.Items.Add(KEY_F16);
            SpecialKeysComboBox.Items.Add(KEY_KEYPAD_ADD);
            SpecialKeysComboBox.Items.Add(KEY_KEYPAD_SUBTRACT);
            SpecialKeysComboBox.Items.Add(KEY_KEYPAD_MULTIPLY);
            SpecialKeysComboBox.Items.Add(KEY_KEYPAD_DIVIDE);

            SpecialKeysComboBox.SelectedIndex = 0;
        }

        #region Keys
        public static string KEY_SHIFT = "+";
        public static string KEY_ALT = "%";
        public static string KEY_LEFT_ARROW = "{LEFT}";
        public static string KEY_RIGHT_ARROW = "{RIGHT}";
        public static string KEY_UP_ARROW = "{UP}";
        public static string KEY_DOWN_ARROW = "{DOWN}";
        public static string KEY_BACKSPACE = "{BACKSPACE}";
        public static string KEY_BREAK = "{BREAK}";
        public static string KEY_CAPS_LOCK = "{CAPSLOCK}";
        public static string KEY_DELETE = "{DELETE}";
        public static string KEY_END = "{END}";
        public static string KEY_ENTER = "{ENTER}";
        public static string KEY_ESC = "{ESC}";
        public static string KEY_HELP = "{HELP}";
        public static string KEY_HOME = "{HOME}";
        public static string KEY_INSERT = "{INSERT}";
        public static string KEY_NUM_LOCK = "{NUMLOCK}";
        public static string KEY_PAGE_DOWN = "{PGDN}";
        public static string KEY_PAGE_UP = "{PGUP}";
        public static string KEY_PRINT_SCREEN = "{PRTSC}";
        public static string KEY_SCROLL_LOCK = "{SCROLLLOCK}";
        public static string KEY_TAB = "{TAB}";
        public static string KEY_F1 = "{F1}";
        public static string KEY_F2 = "{F2}";
        public static string KEY_F3 = "{F3}";
        public static string KEY_F4 = "{F4}";
        public static string KEY_F5 = "{F5}";
        public static string KEY_F6 = "{F6}";
        public static string KEY_F7 = "{F7}";
        public static string KEY_F8 = "{F8}";
        public static string KEY_F9 = "{F9}";
        public static string KEY_F10 = "{F10}";
        public static string KEY_F11 = "{F11}";
        public static string KEY_F12 = "{F12}";
        public static string KEY_F13 = "{F13}";
        public static string KEY_F14 = "{F14}";
        public static string KEY_F15 = "{F15}";
        public static string KEY_F16 = "{F16}";
        public static string KEY_KEYPAD_ADD = "{ADD}";
        public static string KEY_KEYPAD_SUBTRACT = "{SUBTRACT}";
        public static string KEY_KEYPAD_MULTIPLY = "{MULTIPLY}";
        public static string KEY_KEYPAD_DIVIDE = "{DIVIDE}";
        #endregion KeysEnd

        private void Button_AddSpecialButton_Click(object sender, EventArgs e)
        {
            KeyPressEventTextBox.AppendText(SpecialKeysComboBox.SelectedItem.ToString());
        }
    }
}