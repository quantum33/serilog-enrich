namespace ConsoleApp1
{
    public static class PeronMapper
    {
        public static LogEntryPerson ToLogEntryPerson(this Person person)
            => new LogEntryPerson
            {
                Name = person.Name,
                FirstName = person.FirstName,
                MainCity = person.MainAddress.City,
                MainComplement = person.MainAddress.Complement,
                MainCountry = person.MainAddress.Country,
                SecAddress = person.SecondAddress
            };
    }
}