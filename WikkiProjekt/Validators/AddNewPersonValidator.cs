using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikkiDBBlib.Models;

namespace WikkiProjekt.Validators
{
    // Hier muss zuerst über NuGet folgende Klasse hinzugefügt werden: FluentValidation

    // bei google mal suchen: c# lambda expression   weil p=> p.Name = Lambda expression
    public class AddNewPersonValidator : AbstractValidator<WikkiDBBlib.Models.Person>
    {
        // p = Person
        // ctor und 2 x Tab eingeben, das ist ein Konstruktor der dann erzeugt wird
        public AddNewPersonValidator()
        {
            RuleFor(p => p.PName).NotEmpty().WithMessage("Name darf nicht leer sein!");
            RuleFor(p => p.PVorname).NotEmpty().WithMessage("Vorname darf nicht leer sein!");
            RuleFor(p => p.PBild).NotNull().WithMessage("Bitte fügen Sie ein Bild ein!");      // Bild
            RuleFor(p => p.SID).LessThan(0).WithMessage("Bitte wählen Sie eine Stadt aus!");   // Stadt
        }
        
    }
}
