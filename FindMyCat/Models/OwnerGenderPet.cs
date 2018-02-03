using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindMyCat.Models
{
    public class OwnerGenderPet
    {
        public string Gender { get; set; }
        public List<Pet> Pets { get; set; }
    }
}