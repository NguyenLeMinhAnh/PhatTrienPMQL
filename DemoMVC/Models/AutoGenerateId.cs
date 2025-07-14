using DemoMVC.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DemoMVC.Models
{
    public class AutoGenerateId
    {
        public string GenerateId(string inputID)
        {
            //STD008
            var match = System.Text.RegularExpressions.Regex.Match(inputID, @"^(?<prefix>[A-Za-z]+)(?<number>\d+)$");
            if (!match.Success)
            {
                throw new ArgumentException("Invalid id format");
            }
            string prefix = match.Groups["prefix"].Value;
            //STD
            string numberPart = match.Groups["number"].Value;
            //008
            int number = int.Parse(numberPart) + 1;
            //9
            string newNumberPart = number.ToString().PadLeft(numberPart.Length, '0');
            //STD009
            return prefix + newNumberPart;
        }
    }
}
