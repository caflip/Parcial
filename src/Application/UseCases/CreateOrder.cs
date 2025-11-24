using Domain.Entities;
using Domain.Services;
using Infrastructure.Data;
using Infrastructure.Logging;

namespace Application.UseCases;

public static class CreateOrderUseCase
{
    public static Order Execute(string customer, string product, int qty, decimal price)
    {
        Logger.Log("CreateOrderUseCase starting");

        var order = OrderService.CreateOrder(customer, product, qty, price);

        var sql =
            $"INSERT INTO Orders(Id, Customer, Product, Qty, Price) " +
            $"VALUES ({order.Id}, '{customer}', '{product}', {qty}, {price.ToString(System.Globalization.CultureInfo.InvariantCulture)})";


        Logger.Try(() => BadDb.ExecuteNonQueryUnsafe(sql));

        Thread.Sleep(1500);

        return order;
    }
}
