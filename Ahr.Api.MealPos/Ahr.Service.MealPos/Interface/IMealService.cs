﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ahr.Service.MealPos
{
    public interface IMealService : IAppBaseService
    {
        Task<MealDto> CreateMeal(MealDto model);
        Task<MealDto> UpdateMeal(int id, MealDto model);
        Task DeleteMeal(int id);
        Task<MealDto> GetMeal(int id);
        Task<IEnumerable<MealDto>> MealList();
        Task<IEnumerable<MealAddOnDto>> MealAddOnList();
        Task<List<string>> GetUnit();
        Task<List<string>> GetMealType();
    }
}
