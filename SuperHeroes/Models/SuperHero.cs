using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SuperHeroes.Models
{
    public class SuperHero
    {
        [Key]
        public int HeroId { get; set; }
        public string HeroName { get; set; }
        public string AlterEgo { get; set; }
        public string heroPrimaryAbility { get; set; }
        public string HeroSecondaryAbility { get; set; }
        public string CatchPhrase { get; set; }
    }
}