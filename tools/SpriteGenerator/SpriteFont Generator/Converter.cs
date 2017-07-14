using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace SpriteFont_Generator
{
    public class Converter
    {
        #region Constants

        /// <summary>
        /// Cor de fundo da imagem.
        /// </summary>
        private Color BackgroundImageColor = Color.FromArgb(255, 255, 0, 255);

        #endregion

        #region Properties

        /// <summary>
        /// Imagem a ser convertida.
        /// </summary>
        private Bitmap ImageField;

        /// <summary>
        /// Imagem a ser convertida.
        /// </summary>
        public Bitmap Image
        {
            get { return ImageField; }
        }

        /// <summary>
        /// Colunas que devem ser preenchidas.
        /// </summary>
        private List<Int32> ColumnsToFillField;

        /// <summary>
        /// Colunas que devem ser preenchidas.
        /// </summary>
        public List<Int32> ColumnsToFill
        {
            get { return ColumnsToFillField; }
        }

        /// <summary>
        /// Linhas que devem ser preenchidas.
        /// </summary>
        private List<Int32> RowsToFillField;

        /// <summary>
        /// Linhas que devem ser preenchidas.
        /// </summary>
        public List<Int32> RowsToFill
        {
            get { return RowsToFillField; }
        }

        #endregion

        #region Events

        /// <summary>
        /// Assinatura do método que irá tratar o evento de atualização de interface.
        /// </summary>
        /// <param name="message">Mensagem que será passada para a interface.</param>
        public delegate void UpdateUserInterfaceDelegate(string message);

        /// <summary>
        /// Evento que atualiza a interface gráfica.
        /// </summary>
        public event UpdateUserInterfaceDelegate UpdateUserInterface;

        #endregion

        #region Constructor

        /// <summary>
        /// Construtor da classe.
        /// </summary>
        /// <param name="image">Imagem que deve ser convertida.</param>
        public Converter(Bitmap image)
        {
            if (image == null)
            {
                throw new ArgumentException("The parameter 'image' can not be null.");
            }
            else
            {
                this.ImageField = image;

                this.ColumnsToFillField = new List<Int32>();
                this.RowsToFillField = new List<Int32>();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Converte a imagem.
        /// </summary>
        /// <returns>True, se a imagem foi convertida com sucesso ou False, caso contrário.</returns>
        public bool Convert()
        {
            this.RaiseUpdateUserInterface("Please wait, making some calculations...");

            for (int column = 0; column < this.Image.Width; column++)
            {
                if (this.IsTransparentColumn(column))
                {
                    this.ColumnsToFill.Add(column);
                }
            }

            for (int row = 0; row < this.Image.Height; row++)
            {
                if (this.IsTransparentRow(row))
                {
                    this.RowsToFill.Add(row);
                }
            }

            this.ColumnsToFill.Sort();
            this.RowsToFill.Sort();

            int spacewidth = 0;

            for (int index = 1; index < this.ColumnsToFill.Count; index++)
            {
                int distance = this.ColumnsToFill[index] - this.ColumnsToFill[index - 1];

                if (distance > 1)
                {
                    spacewidth += distance;
                }
            }

            spacewidth /= (127 - 32);

            this.ColumnsToFill.RemoveRange(2, spacewidth);

            try
            {
                this.RedrawImage();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Verifica se a coluna especificada é transparente.
        /// </summary>
        /// <param name="column">Coluna a ser verificada.</param>
        /// <returns>True, se a coluna não possuir nenhum pixel com cor.</returns>
        private bool IsTransparentColumn(int column)
        {
            for (int row = 0; row < this.Image.Height; row++)
            {
                if (this.Image.GetPixel(column, row).A != 0)
                {
                    return false;
                }
            }

            return true;
        }
        
        /// <summary>
        /// Verifica se a linha especificada é transparente.
        /// </summary>
        /// <param name="column">Linha a ser verificada.</param>
        /// <returns>True, se a linha não possuir nenhum pixel com cor.</returns>
        private bool IsTransparentRow(int row)
        {
            for (int column = 0; column < this.Image.Width; column++)
            {
                if (this.Image.GetPixel(column, row).A != 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Pinta todos os pixeis da coluna especificada, com a cor de fundo.
        /// </summary>
        /// <param name="column">Coluna a ser pintada.</param>
        private void FillColumn(int column)
        {
            for (int row = 0; row < this.Image.Height; row++)
            {
                this.Image.SetPixel(column, row, this.BackgroundImageColor);
            }

            if (column % 5 == 0)
            {
                this.RaiseUpdateUserInterface("Please wait, redrawing the image...");
            }
        }

        /// <summary>
        /// Pinta todos os pixeis da linha especificada, com a cor de fundo.
        /// </summary>
        /// <param name="column">Linha a ser pintada.</param>
        private void FillRow(int row)
        {
            for (int column = 0; column < this.Image.Width; column++)
            {
                this.Image.SetPixel(column, row, this.BackgroundImageColor);
            }

            if (row % 5 == 0)
            {
                this.RaiseUpdateUserInterface("Please wait, redrawing the image...");
            }
        }

        /// <summary>
        /// Redesenha a imagem, apagando as linhas e colunas necessárias.
        /// </summary>
        private void RedrawImage()
        {
            this.RaiseUpdateUserInterface("Please wait, redrawing the image...");

            foreach (int column in this.ColumnsToFill)
            {
                this.FillColumn(column);
            }

            foreach (int row in this.RowsToFill)
            {
                this.FillRow(row);
            }

            this.RaiseUpdateUserInterface("Done.");
        }

        /// <summary>
        /// Dispara o evento de atualização da interface gráfica.
        /// </summary>
        /// <param name="message">Mensagem que deve ser passada para a interface.</param>
        private void RaiseUpdateUserInterface(string message)
        {
            if (this.UpdateUserInterface != null)
            {
                this.UpdateUserInterface(message);
            }
        }

        #endregion
    }
}
