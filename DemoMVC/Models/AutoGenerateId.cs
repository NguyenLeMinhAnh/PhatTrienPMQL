using DemoMVC.Data;
using Microsoft.EntityFrameworkCore;

namespace DemoMVC.Models
{
    public class AutoGenerateId
    {
        private readonly ApplicationDbContext _context;

        public AutoGenerateId(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> GeneratePersonIdAsync()
        {
            var last = await _context.Persons
                .OrderByDescending(p => p.PersonId) //Sắp xếp PersonId từ lớn đến bé
                .Select(p => p.PersonId) //Chọn cột PersonId
                .FirstOrDefaultAsync(); //Chọn PersonId lớn nhất

            int next = 1;
            if (!string.IsNullOrEmpty(last) && last.Length >= 5) //Chỉ đúng nếu "last" có giá trị và độ dài lớn hơn hoặc bằng 4
            {
                int.TryParse(last.Substring(1), out next);
                next++;
            }

            return "P" + next.ToString("D4");
        }
    }
}
