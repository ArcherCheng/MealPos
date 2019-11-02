using Ahr.Data.MealPos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task<MealDto> CreateMeal(MealDto model)
        {
            using(var db = NewDb())
            {
                var meal = _mapper.Map<MealDto, Meal>(model);
                db.Meal.Add(meal);
                await db.SaveChangesAsync();
                var result = _mapper.Map<Meal, MealDto>(meal);
                return result;
            }
        }

        public async Task DeleteMeal(int id)
        {
            using(var db = NewDb())
            {
                var meal = await db.Meal.FindAsync(id);
                if (meal == null)
                    return;

                db.Meal.Remove(meal);
                await db.SaveChangesAsync();
            }
        }

        public async Task<MealDto> GetMeal(int id)
        {
            using(var db = NewDb())
            {
                var meal = await db.Meal.FindAsync(id);
                var dto = _mapper.Map<Meal, MealDto>(meal);
                return dto;
            }
        }

        public async Task<IEnumerable<MealDto>> MealList()
        {
            using(var db = NewDb())
            {
                var meals = await db.Meal.ToListAsync();
                var dtos = _mapper.Map<IEnumerable<Meal>, IEnumerable<MealDto>>(meals);
                return dtos;
            }
        }

        public async Task<MealDto> UpdateMeal(int id, MealDto model)
        {
            using(var db = NewDb())
            {
                var meal = await db.Meal.FindAsync(id);
                _mapper.Map<MealDto, Meal>(model, meal);
                db.Meal.Update(meal);
                await db.SaveChangesAsync();
                var dto = _mapper.Map<Meal, MealDto>(meal);
                return dto;
            }
        }
    }
}
