int ore;
double salariu;
double tarif;
Console.WriteLine("Introduceti numarul de ore lucrate:");
ore = int.Parse(Console.ReadLine());
Console.WriteLine("Introduceti tariful pe ora:");
tarif = double.Parse(Console.ReadLine());   
salariu = ore * tarif;
Console.WriteLine("Salariul este: " + salariu);
if (salariu > 3000)
{
    Console.WriteLine("Salariul este mare.");
}
else
{
    Console.WriteLine("Salariul este mic");
}

