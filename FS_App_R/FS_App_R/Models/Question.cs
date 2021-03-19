using System;
using System.Collections.Generic;
using System.Text;

namespace FS_App_R.Models
{
    public class Question
    {
    public string Name { get; set; }
    public string Descripton { get; set; }

    public int Id { get; set; }

    public bool IsCheckedYes { get; set; }

    public bool IsCheckedNo { get; set; }

    public string NombreGrupo { get; set; }

    }
}
