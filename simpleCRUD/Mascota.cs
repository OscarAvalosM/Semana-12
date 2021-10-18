using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace simpleCRUD
{
    class mascota
    {
        //propiedades
        public int _mascota { get; set; }
        public string _edad { get; set; }
        public string _color { get; set; }
       

        //instancia a la clase Crud
        private Crud crud = new Crud();

        //metodo para retornar los registros de la tabla Book
        public MySqlDataReader getAllBooks()
        {
            string query = "SELECT bookId,title,subtitle,ISBN,publishedDate FROM book";

            //llamado al metodo select de la clase Crud
            return crud.select(query);
        }

        //metodo para insertar o editar un registro
        public Boolean newEditBook(string action)
        {
            if (action == "new")
            {
                string query = "INSERT INTO book(title, subtitle, ISBN, publishedDate)" +
                    "VALUES ('" + _edad + "', '" + _color + "', '" + "')";
                crud.executeQuery(query); //llamato al metodo executeQuery de la clase Crud
                return true;
            }
            else if (action == "edit")
            {
                string query = "UPDATE book SET "
                    + "edad='" + _edad + "' ,"
                    + "color='" + _color + "',"
                    + "WHERE "
                    + "mascota='" + _mascota + "'";
                crud.executeQuery(query);
                return true;
            }

            return false;
        }

        //metodo para eliminar
        public Boolean deleteBook()
        {
            string query = "DELETE FROM book WHERE bookId='" + _mascota + "'";
            crud.executeQuery(query);
            return true;
        }
    }
}