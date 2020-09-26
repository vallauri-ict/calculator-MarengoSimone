using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculatorForm_Project
{
    public partial class FormMain : Form
    {
        struct ButtonStruct
        {
            public char Content;
            public bool IsBold;
            public bool IsNumber;
            public bool IsDecimalSeparator;
            public bool IsPlusMinusSign;
            public ButtonStruct(char content, bool isBold, bool isNumber=false, bool isDecimalSeparator = false, bool isPlusMinusSign=false)
            {
                this.Content = content;
                this.IsBold = isBold;
                this.IsNumber = isNumber;
                this.IsDecimalSeparator = isDecimalSeparator;
                this.IsPlusMinusSign = isPlusMinusSign;
            }
            public override string ToString()
            {
                return Content.ToString();
            }
        }
        
        //private char[,] buttons = new char [6,4];
        private ButtonStruct[,] buttons =
        {
            {new ButtonStruct(' ',false), new ButtonStruct(' ',false), new ButtonStruct(' ',false), new ButtonStruct(' ',false) },
            {new ButtonStruct(' ',false), new ButtonStruct(' ',false), new ButtonStruct(' ',false), new ButtonStruct('/',false) },
            {new ButtonStruct('7',true,true), new ButtonStruct('8',true,true), new ButtonStruct('9',true,true), new ButtonStruct('x',false) },
            {new ButtonStruct('4',true,true), new ButtonStruct('5',true,true), new ButtonStruct('6',true,true), new ButtonStruct('-',false) },
            {new ButtonStruct('1',true,true), new ButtonStruct('2',true,true), new ButtonStruct('3',true,true), new ButtonStruct('+',false) },
            {new ButtonStruct('±',false,false,false,true), new ButtonStruct('0',true,true), new ButtonStruct(',',false,false,true), new ButtonStruct('=',true) }
        };

        private RichTextBox resultBox;
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            MakeResultBox();
            MakeButtons(buttons);
            
        }

        private void MakeResultBox()
        {
            resultBox = new RichTextBox();
            resultBox.ReadOnly = true;
            resultBox.SelectionAlignment = HorizontalAlignment.Right;
            resultBox.Font = new Font("Segoe UI", 22);
            resultBox.Width = this.Width - 16;
            resultBox.Height = 50;
            resultBox.Top = 20;
            resultBox.Text = "0";
            resultBox.TabStop = false;
            this.Controls.Add(resultBox);
        }

        private void MakeButtons(ButtonStruct[,] buttons)
        {
            int buttonWidth = 82;
            int buttonHeight = 60;
            int posX = 0;
            int posY = 101;
            for (int i = 0; i < buttons.GetLength(0); i++)
            {
                for (int j = 0; j < buttons.GetLength(1); j++)
                {
                    Button newButton = new Button();
                    newButton.Font = new Font("Segoe UI", 16);
                    ButtonStruct bs = buttons[i, j];
                    //newButton.Text = bs.Content.ToString();
                    newButton.Text = buttons[i, j].ToString();
                    if(bs.IsBold)
                    {
                        newButton.Font = new Font(newButton.Font, FontStyle.Bold);
                    }
                    newButton.Width = buttonWidth;
                    newButton.Height = buttonHeight;
                    newButton.Left = posX;
                    newButton.Top = posY;
                    newButton.Tag = bs;
                    newButton.Click += Button_Click;
                    this.Controls.Add(newButton);
                    posX += buttonWidth;
                }
                posX = 0;
                posY += buttonHeight;
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            ButtonStruct bs = (ButtonStruct)clickedButton.Tag;
            if(bs.IsNumber)
            {
                if (resultBox.Text == "0")
                {
                    resultBox.Text = "";
                }
                resultBox.Text += clickedButton.Text;
            }
            else
            {
                if (bs.IsDecimalSeparator)
                {
                    if (!resultBox.Text.Contains(bs.Content))
                    {
                        resultBox.Text += clickedButton.Text;
                    }
                }
                if (bs.IsPlusMinusSign)
                {
                    if(!resultBox.Text.Contains("-"))
                    {
                        resultBox.Text = "-" + resultBox.Text;
                    }
                    else
                    {
                        resultBox.Text = resultBox.Text.Replace("-", "");
                    }
                }
            }
        }
    }
}
