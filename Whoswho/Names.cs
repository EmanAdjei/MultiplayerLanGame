using System;
using System.Collections.Generic;

namespace Whoswho
{
    public class Names
    {
        // List of names used to look up character images on the client side
        private List<String> nameCharacter = new List<String>()
        {
            "Jessica", "Ruby", "Thomas", "Fleur", "Sara", "Amir", "Hugo",
            "Lucy", "Ayesha", "Alexandre", "Lucas", "Adele", "Simon",
            "Antonio", "Edward", "Mateo", "Daniel", "Cameron",
            "Gabriel", "Amelie", "Diego", "Sofia", "Roberto", "Zoe"
        };

        public List<string> NameCharacter { get => nameCharacter; set => nameCharacter = value; }
    }
}
