using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Threading;
using System.IO;

namespace SpriteFont_Generator
{
    public partial class FrmMain : Form
    {
        #region Properties

        /// <summary>
        /// Imagem de fundo do formulário que deverá ser convertida.
        /// </summary>
        private Bitmap ImageToConvertField;

        /// <summary>
        /// Imagem de fundo do formulário que deverá ser convertida.
        /// </summary>
        private Bitmap ImageToConvert
        {
            get { return ImageToConvertField; }
            set { ImageToConvertField = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Construtor da classe.
        /// </summary>
        public FrmMain()
        {
            InitializeComponent();

            saveAsToolStripMenuItem.Enabled = false;
            generateToolStripMenuItem.Enabled = false;
        }

        #endregion

        #region Menu

        /// <summary>
        /// Menu Open
        /// </summary>
        private void Open_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();

            openDialog.InitialDirectory = Environment.SpecialFolder.DesktopDirectory.ToString();
            openDialog.Multiselect = false;
            openDialog.Filter = "Image Files|*.bmp;*.jpg;*.png;*.gif";

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                this.ImageToConvert = Bitmap.FromFile(openDialog.FileName) as Bitmap;

                this.pictureBox.Image = this.ImageToConvert;

                saveAsToolStripMenuItem.Enabled = true;
                generateToolStripMenuItem.Enabled = true;

                this.toolStripStatusLabel.Text = "Image loaded sucessfully.";
            }
        }
        
        /// <summary>
        /// Menu Save As
        /// </summary>
        private void SaveAs_Click(object sender, EventArgs e)
        {
            if (this.ImageToConvert != null)
            {
                SaveFileDialog saveDialog = new SaveFileDialog();

                saveDialog.InitialDirectory = Environment.SpecialFolder.DesktopDirectory.ToString();
                saveDialog.Filter = "Png File|*.png;";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveDialog.FileName;

                    if (!fileName.Substring(fileName.Length - 4).Equals(".png", StringComparison.InvariantCultureIgnoreCase))
                    {
                        fileName = String.Format("{0}.png", fileName);
                    }

                    this.ImageToConvert.Save(fileName, ImageFormat.Png);

                    this.toolStripStatusLabel.Text = "Image saved sucessfully.";
                }
            }
        }

        /// <summary>
        /// Menu Sair
        /// </summary>
        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Menu Generate
        /// </summary>
        private void Generate_Click(object sender, EventArgs e)
        {
            if (this.ImageToConvert != null)
            {
                Converter converter = new Converter(this.ImageToConvert);

                converter.UpdateUserInterface += new Converter.UpdateUserInterfaceDelegate(UpdateUI);

                try
                {
                    if (converter.Convert())
                    {
                        this.ImageToConvertField = converter.Image;
                        generateToolStripMenuItem.Enabled = false;
                    }
                }
                catch (Exception error)
                {
                    this.ShowError(error);
                }

                this.toolStripStatusLabel.Text = "SpriteFont generated sucessfully.";
            }
        }

        /// <summary>
        /// Menu Automation
        /// </summary>
        private void Automation_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();

            folderDialog.Description = "Select the folder where are the images to be converted:";
            folderDialog.RootFolder = Environment.SpecialFolder.Desktop;
            folderDialog.ShowNewFolderButton = false;

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                DirectoryInfo directory = new DirectoryInfo(folderDialog.SelectedPath);

                try
                {
                    List<FileInfo> files = new List<FileInfo>();

                    files.AddRange(directory.GetFiles("*.bmp"));
                    files.AddRange(directory.GetFiles("*.jpg"));
                    files.AddRange(directory.GetFiles("*.png"));
                    files.AddRange(directory.GetFiles("*.gif"));

                    for (int index = 0; index < files.Count; index++)
                    {
                        this.toolStripStatusLabel.Text = String.Format("Generating spritefont {0} of {1}.", index + 1, files.Count);

                        Application.DoEvents();
                        Converter converter = new Converter(Bitmap.FromFile(files[index].FullName) as Bitmap);

                        if (converter.Convert())
                        {
                            converter.Image.Save(String.Format("{0}_Sprite.png", files[index].FullName.Substring(0, files[index].FullName.Length - 4)), ImageFormat.Png);
                        }
                    }
                }
                catch (Exception error)
                {
                    this.ShowError(error);
                }

                this.toolStripStatusLabel.Text = String.Format("Automation executed sucessfully in {0}.", directory.FullName);
            }
        }
        
        #endregion

        #region Converter Events

        /// <summary>
        /// Atualiza o formulário.
        /// </summary>
        /// <param name="message">Mensagem que deve ser escrita na barra de status.</param>
        private void UpdateUI(string message)
        {
            this.pictureBox.Refresh();
            this.toolStripStatusLabel.Text = message;
            Application.DoEvents();
        }

        #endregion

        #region Error Handling

        /// <summary>
        /// Exibe uma mensagem de erro.
        /// </summary>
        /// <param name="error">Mensagem a ser exibida.</param>
        private void ShowError(Exception error)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine(error.Message);
            builder.AppendLine();
            builder.AppendLine(error.StackTrace);

            MessageBox.Show(builder.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion
    }
}