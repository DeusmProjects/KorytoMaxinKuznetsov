using KorytoModel;
using KorytoServiceDAL.BindingModel;
using KorytoServiceDAL.Interfaces;
using KorytoServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KorytoServiceImplementDataBase.Implementations
{
    public class CarServiceDB : ICarService
    {
        readonly KorytoDbContext context;

        public CarServiceDB(KorytoDbContext context)
        {
            this.context = context;
        }

        public void AddElement(CarBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var car = context.Cars.FirstOrDefault(record => record.CarName == model.CarName);

                    if (car != null)
                    {
                        throw new Exception("Такое авто уже существует.");
                    }
                    else
                    {
                        car = new Car
                        {
                            CarName = model.CarName,
                            Price = model.Price,
                            Year = model.Year
                        };
                    }
                   
                    context.Cars.Add(car);
                    context.SaveChanges();

                    var duplicates = model.CarDetails
                        .GroupBy(record => record.DetailId)
                        .Select(record => new
                        {
                            detailId = record.Key,
                            amount = record.Sum(rec => rec.Amount)
                        });

                    foreach (var duplicate in duplicates)
                    {
                        context.CarDetails.Add(new CarDetail
                        {
                            CarId = car.Id,
                            DetailId = duplicate.detailId,
                            Amount = duplicate.amount
                        });
                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void DeleteElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var car = context.Cars.FirstOrDefault(record => record.Id == id);

                    if (car != null)
                    {
                        context.CarDetails.RemoveRange(
                            context.CarDetails.Where(
                                record => record.CarId == id));

                        context.Cars.Remove(car);

                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Автомобиль не найден");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public CarViewModel GetElement(int id)
        {
            var car = context.Cars.FirstOrDefault(record => record.Id == id);

            if (car != null)
            {
                return new CarViewModel
                {
                    Id = car.Id,

                    CarName = car.CarName,

                    Price = car.Price,

                    Year = car.Year,

                    CarDetails = context.CarDetails
                        .Where(recordCD => recordCD.CarId == car.Id)
                        .Select(recCD => new CarDetailViewModel
                        {
                            Id = recCD.Id,
                            CarId = recCD.CarId,
                            DetailId = recCD.DetailId,
                            DetailName = recCD.Detail.DetailName,
                            Amount = recCD.Amount
                        }).ToList()
                };
            }
            throw new Exception("Автомобиль не найден");
        }

        public List<CarViewModel> GetFilteredList()
        {
            var result = new List<CarViewModel>();

            var cars = context.Cars.Select(rec => new CarViewModel
            {
                Id = rec.Id,
                CarName = rec.CarName,
                Year = rec.Year,
                Price = rec.Price,
                CarDetails = context.CarDetails
                .Where(recCD => recCD.CarId == rec.Id)
                .Select(recCD => new CarDetailViewModel
                {
                    Id = recCD.Id,
                    CarId = recCD.CarId,
                    DetailId = recCD.DetailId,
                    DetailName = recCD.Detail.DetailName,
                    Amount = recCD.Amount
                }).ToList()
            }).ToList();

            foreach (var car in cars)
            {
                var details = car.CarDetails.Select(rec => new DetailViewModel
                {
                    Id = rec.DetailId,
                    TotalAmount = context.Details.FirstOrDefault(recD => recD.Id == rec.DetailId).TotalAmount
                }).ToList();

                if (details.All(rec => rec.TotalAmount >= car.CarDetails.FirstOrDefault(recCD => recCD.DetailId == rec.Id).Amount))
                {
                    result.Add(car);
                }
            }

            return result;
        }

        public List<CarViewModel> GetList()
        {
            List<CarViewModel> result = context.Cars.Select(record => new CarViewModel
            {
                Id = record.Id,
                CarName = record.CarName,
                Price = record.Price,
                Year = record.Year,
                CarDetails = context.CarDetails.Where(recordCarDetails => recordCarDetails.CarId == record.Id).Select(recordCarDetails => new CarDetailViewModel
                {
                    Id = recordCarDetails.Id,
                    CarId = recordCarDetails.CarId,
                    DetailId = recordCarDetails.DetailId,
                    DetailName = recordCarDetails.Detail.DetailName,
                    Amount = recordCarDetails.Amount
                }).ToList()

            }).ToList();

            return result;
        }    

        public void UpdateElement(CarBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var car = context.Cars
                        .FirstOrDefault(record => record.CarName == model.CarName && record.Id != model.Id);

                    if (car != null)
                    {
                        throw new Exception("Уже есть автомобиль с таким названием");
                    }

                    car = context.Cars
                        .FirstOrDefault(record => record.Id == model.Id);

                    if (car == null)
                    {
                        throw new Exception("Автомобиль не найден");
                    }

                    car.CarName = model.CarName;
                    car.Price = model.Price;
                    car.Year = model.Year;
                    context.SaveChanges();

                    var IDs = model.CarDetails.Select(
                        record => record.DetailId)
                        .Distinct();

                    var updateDetails = context.CarDetails.Where(
                        record => record.CarId == model.Id && IDs.Contains(record.DetailId));

                    foreach (var updateDetail in updateDetails)
                    {
                        updateDetail.Amount = model.CarDetails.FirstOrDefault(record => record.Id == updateDetail.Id).Amount;
                    }

                    context.SaveChanges();

                    context.CarDetails
                        .RemoveRange(context.CarDetails
                            .Where(record => record.CarId == model.Id && !IDs.Contains(record.DetailId)));

                    context.SaveChanges();

                    var groupDetails = model.CarDetails
                        .Where(record => record.Id == 0)
                        .GroupBy(record => record.DetailId)
                        .Select(record => new {detailId = record.Key, amount = record.Sum(r => r.Amount)});

                    foreach (var groupDetail in groupDetails)
                    {
                        var detail = context.CarDetails
                            .FirstOrDefault(record => record.CarId == model.Id && record.DetailId == groupDetail.detailId);

                        if (detail != null)
                        {
                            detail.Amount += groupDetail.amount;
                            context.SaveChanges();
                        }
                        else
                        {
                            context.CarDetails.Add(new CarDetail
                            {
                                CarId = model.Id,
                                DetailId = groupDetail.detailId,
                                Amount = groupDetail.amount
                            });

                            context.SaveChanges();
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
