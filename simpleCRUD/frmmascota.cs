using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace simpleCRUD
{
    public partial class frmBook : Form
    {
        private string action = "";
        public frmBook()
        {
            InitializeComponent();
        }
      

        private void Form1_Load(object sender, EventArgs e)
        {
            //deja un tab 
            tabs.TabPages.Remove(tabForm);

            //carga los datos en el datagridView
            //deshabilita controles
            fillDataGridView();
            controlsDisable();

        }

        //utilizado para mostrar los registros en el datagridview
        public void fillDataGridView()
        {
            //instancia de la clase libro| Book
            mascota book = new mascota();

            clearDataGridView();

            dtgmascota.Columns.Add("bookId", "BOOK ID");
            dtgmascota.Columns.Add("title", "TITLE");
            dtgmascota.Columns.Add("subtitle", "SUBTITLE");
            

            //llamado al medoto getBooks() de la clase Book
            MySqlDataReader dataReader = book.getAllBooks();

            //leer el resultado y mostrarlo en el datagridview
            while(dataReader.Read())
            {
                dtgmascota.Rows.Add(
                        dataReader.GetValue(0),
                        dataReader.GetValue(1),
                        dataReader.GetValue(2),
                        dataReader.GetValue(3),
                        dataReader.GetValue(4)
                       );
            }
        }

        public void clearDataGridView()
        {
            dtgmascota.Columns.Clear();
            dtgmascota.Rows.Clear();
        }
        //deshabilita los controles de formulario
        public void controlsDisable()
        {
            txtId.Enabled = false;
            txtTitle.Enabled = false;
            txtSubTitle.Enabled = false;
           
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
        }
        //habilitar los controles de formulario
        public void controlsEnable()
        {
            txtId.Enabled = false;
            txtTitle.Enabled = true;
            txtSubTitle.Enabled = true;
           
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
        }
        //limpiar el contenido de los controles
        public void clearControls()
        {
            txtId.Text = "";
            txtTitle.Text = "";
            txtSubTitle.Text = "";
            
        }

          
       
        private void dtgBooks_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            tabs.TabPages.Remove(tabData);//ocultar el tab de el datagridview
            tabs.TabPages.Add(tabForm); //mostrar el formulario para los datos
            tabs.TabPages[0].Text = "EDIT BOOK";

            action = "edit";
            controlsEnable();

            txtId.Visible = true;
            txtId.ReadOnly = true;
            lblId.Visible = true;

            //cargar datos en controles
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            //mediante este boton se programara para guardar y editar
        }

      

        private void btnExit_Click(object sender, EventArgs e)
        {
            //codigo del boton salir
            string mensaje = "¿Está seguró que desea salir?";
            if (MetroFramework.MetroMessageBox.Show(this, mensaje, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
            {
                this.Close();
            }

        }

        private void lNew_Click(object sender, EventArgs e)
        {
            tabs.TabPages.Add(tabForm);//mostrar el formulario
            tabs.TabPages.Remove(tabData); //ocultar el  tab del dataagridview
            tabs.TabPages[0].Text = "NEW MASCOTA"; //agregar texto al tab

            txtId.Visible = false;
            lblId.Visible = false;
            txtTitle.Focus(); //enviar enfoque al titulo
            action = "new";
            controlsEnable();
            clearControls();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            string mensaje = "¿Está seguró que desea cancelar?";
            if (MetroFramework.MetroMessageBox.Show(this, mensaje, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                clearControls();
                controlsDisable();


                tabs.TabPages.Remove(tabForm);
                tabs.TabPages.Add(tabData);
                tabs.TabPages[0].Text = "MASCOTA LIST";
            }
        }

        private void tabForm_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtTitle.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Debe escribir el titulo", "VALIDACION",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitle.Focus(); //enviamos el enfoque a la caja de texto
                
            }
            else
            {

                mascota book = new mascota(); //instancia de la clase Libro
                                        //evaluar la accion
                if (action == "edit")
                {
                    mascota._mascota = Convert.ToInt32(txtId.Text);
                }


                mascota._edad = txtTitle.Text;
                mascota._color = txtSubTitle.Text;
                

                string mensaje = "Esta seguro que desea guardar el registro?";
                if (MetroFramework.MetroMessageBox.Show(this, mensaje, "CONFIRMACION",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //LLAMAR AL METODO PARA GUARDAR
                    if (book.newEditBook(action))
                    {
                        MetroFramework.MetroMessageBox.Show(this, "Los datos se han guardado exitosamente!",
                            "CONFIRMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MetroFramework.MetroMessageBox.Show(this, "Los datos  no se han guardado!",
                            "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    clearControls();
                    controlsDisable();
                    fillDataGridView();
                    tabs.TabPages.Remove(tabForm);
                    tabs.TabPages.Add(tabData);
                    tabs.TabPages[0].Text = "MASCOTA LIST";
                }
            }
        }

        private void edit_Click(object sender, EventArgs e)
        {
            tabs.TabPages.Remove(tabData);
            tabs.TabPages.Add(tabForm);
            tabs.TabPages[0].Text = "EDIT BOOK";
            controlsEnable();

            txtId.Visible = true;
            txtId.ReadOnly = true;
            lblId.Visible = true;

            //pasar los valores del datagridview hacia los texbox
            txtId.Text = dtgmascota.CurrentRow.Cells[0].Value.ToString();
            txtTitle.Text = dtgmascota.CurrentRow.Cells[1].Value.ToString();
            txtSubTitle.Text = dtgmascota.CurrentRow.Cells[2].Value.ToString();
          
            //enviar aaccion
            action = "edit";
        }

        private void delete_Click(object sender, EventArgs e)
        {
            string mensaje = "Esta seguro que desea eliminar el registro?";
            if (MetroFramework.MetroMessageBox.Show(this, mensaje, "CONFIRMACION",
               MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                mascota book = new mascota();
                book._mascota = Convert.ToInt32(dtgmascota.CurrentRow.Cells[0].Value);

                //llamado al metodo deleteBook() de la clase Book
                if (book.deleteBook())
                {
                    MetroFramework.MetroMessageBox.Show(this, "Los datos se han eliminado exitosamente!",
                        "CONFIRMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //actualizar datagridview
                    fillDataGridView();

                }
                else
                {
                    MetroFramework.MetroMessageBox.Show(this, "Los datos  no se han podido eliminar",
                        "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

}
