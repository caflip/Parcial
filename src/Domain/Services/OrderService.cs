using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Services;

public static class OrderService
{
    private static readonly List<Order> _lastOrders = [];

    public static IReadOnlyList<Order> LastOrders => _lastOrders.AsReadOnly();

    public static Order CreateOrder(string customer, string product, int qty, decimal price)
    {
        var order = new Order
        {
            Id = Random.Shared.Next(1, 9_999_999),
            CustomerName = customer,
            ProductName = product,
            Quantity = qty,
            UnitPrice = price
        };

        _lastOrders.Add(order);
        return order;
    }
}
