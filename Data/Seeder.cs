using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using pet_manager.Data;
using pet_manager.Models;

namespace pet_manager
{
    public class Seeder{
        private UserManager<Owner> _userManager;
        private readonly ApplicationDbContext _context;

        public Seeder(ApplicationDbContext context, UserManager<Owner> userManager)
        {
         _context = context;  
         _userManager = userManager; 
        }

        public async Task Seed(){
            if(_context.Pets.Any())
                return;

            var user = new Owner{Email="manjuregmiuprety@gmail.com",
                            UserName = "manjuregmiuprety@gmail.com",
                            Name="Manju", 
                            Address="Wellington",
                            Contact="123456789"};

            var result = await _userManager.CreateAsync(user,"P@ssw0rd");

            if(!result.Succeeded)
                return;

            var pets = new List<Pet>{
                new Pet{Name="Cat",Age=1, Color="Black",Location="NZ",Description="Lovely",Owner = user},
                new Pet{Name="Dog",Age=1, Color="Grey",Location="NZ",Description="Lovely dog",Owner = user},
                new Pet{Name="Goat",Age=2, Color="Black",Location="NZ",Description="Sweet",Owner = user},
                new Pet{Name="Horse",Age=1, Color="Green",Location="NZ",Description="Lovely",Owner = user},
                new Pet{Name="Rabbit",Age=1, Color="Black",Location="NZ",Description="Lovely",Owner = user},
                new Pet{Name="Mouse",Age=1, Color="Black",Location="NZ",Description="Lovely",Owner = user},
                new Pet{Name="Cow",Age=1, Color="Orange",Location="NZ",Description="Lovely",Owner = user},
                new Pet{Name="Rat",Age=1, Color="Black",Location="NZ",Description="Lovely",Owner = user},
                new Pet{Name="Hen",Age=1, Color="Black",Location="NZ",Description="Lovely",Owner = user},
                new Pet{Name="Chicken",Age=1, Color="Black",Location="NZ",Description="Lovely",Owner = user},
                new Pet{Name="Bird",Age=1, Color="Black",Location="NZ",Description="Lovely",Owner = user},
                new Pet{Name="Parrot",Age=1, Color="Black",Location="NZ",Description="Lovely",Owner = user},                
            };

            await _context.Pets.AddRangeAsync(pets);
            await _context.SaveChangesAsync();
        }
    }
}