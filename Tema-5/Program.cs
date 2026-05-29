var inStock = products
.Where(p => p.Stock > 0)
.OrderBy(p => p.Price);
Console.WriteLine("\n Товары в наличии:");
foreach (var p in inStock)
Console.WriteLine($"  {p.Name} — {p.Price} ₽ (в наличии: {p.Stock})");
