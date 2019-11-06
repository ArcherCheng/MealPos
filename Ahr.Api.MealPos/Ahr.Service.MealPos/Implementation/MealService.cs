using Ahr.Data.MealPos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahr.Service.MealPos
{
    public class MealService : AppBaseService, IMealService
    {
        private readonly IMapper _mapper;

        public MealService(IMapper mapper)
        {
            this._mapper = mapper;
        }

        public async Task<MealDto> GetMeal(int id)
        {
            using (var db = NewDb())
            {
                var meal = await db.Meal
                    .Include(x => x.MealAddOnRela)
                    .ThenInclude(rela => rela.AddOn)
                    .FirstAsync(x => x.Id == id);

                var dto = _mapper.Map<Meal, MealDto>(meal);
                return dto;
            }
        }

        public async Task<IEnumerable<MealDto>> MealList()
        {
            using (var db = NewDb())
            {
                var meals = await db.Meal
                    .Include(x => x.MealAddOnRela)
                    .ThenInclude( rela  => rela.AddOn )
                    .ToListAsync();

                var dtos = _mapper.Map<IEnumerable<Meal>, IEnumerable<MealDto>>(meals);
                return dtos;
            }
        }

        public async Task<MealDto> CreateMeal(MealDto model)
        {
            using(var db = NewDb())
            {
                var meal = _mapper.Map<MealDto, Meal>(model);
                ///方法二
                ///var addOn = model.MealAddOnDtos.Select(x => new MealAddOnRela { AddOnId = x.Id });
                ///meal.MealAddOnRela = addOn.ToList();

                db.Meal.Add(meal);
                await db.SaveChangesAsync();
                return await GetMeal(meal.Id);
                //retutn model;
            }
        }

        public async Task DeleteMeal(int id)
        {
            using(var db = NewDb())
            {
                var meal = await db.Meal
                    .Include(x => x.MealAddOnRela)
                    .FirstAsync(x =>x.Id == id);
                if (meal == null)
                    return;

                db.MealAddOnRela.RemoveRange(meal.MealAddOnRela);
                db.Meal.Remove(meal);
                await db.SaveChangesAsync();
                //context.Database.ExecuteSqlCommand("Delete Products Where ProductID = {0}", productid);
            }
        }

        public async Task<MealDto> UpdateMeal(int id, MealDto model)
        {
            using(var db = NewDb())
            {
                var meal = await db.Meal.Include(x=>x.MealAddOnRela).FirstAsync(x=>x.Id == id);
                _mapper.Map<MealDto, Meal>(model, meal);
                db.Meal.Update(meal);
                await db.SaveChangesAsync();
                return await GetMeal(id);
                //return model;
            }
        }

        public async Task<IEnumerable<MealAddOnDto>> MealAddOnList()
        {
            using(var db= NewDb())
            {
                var addOn = await db.MealAddOn.ToListAsync();
                var dto = _mapper.Map<IEnumerable<MealAddOn>, IEnumerable<MealAddOnDto>>(addOn);
                return dto;
            }
        }

        public async Task<List<string>> GetUnit()
        {
            var list = await base.GetAppKeyValue("Unit");
            return list.ToList();
        }

        public async Task<List<string>> GetMealType()
        {
            var list = await base.GetAppKeyValue("MealType");
            return list.ToList();
        }
    }
}
