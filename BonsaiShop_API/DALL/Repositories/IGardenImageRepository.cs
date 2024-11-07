﻿using BonsaiShop_API.Areas.Garden.Models;

namespace BonsaiShop_API.DALL.Repositories
{
    public interface IGardenImageRepository
    {
        Task AddImageAsync(int gardenId, GardenImageDTO gardenImageDto);
        Task DeleteImageAsync(int gardenId, int imageId);
    }
}