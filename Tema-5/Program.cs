var inStock = products
.Where(p => p.Stock > 0)
.OrderBy(p => p.Price);
Console.WriteLine("\n Товары в наличии:");
foreach (var p in inStock)
Console.WriteLine($"  {p.Name} — {p.Price} ₽ (в наличии: {p.Stock})");
var byCategory = products
.GroupBy(p => p.Category)
.Select(g => new
{
Category = g.Key,
Count = g.Count(),
AvgPrice = g.Average(p => p.Price)
}]
.OrderByDescending(x => x.Count);
Console.WriteLine("\n Статистика по категориям:");
foreach (var c in byCategory)
Console.WriteLine($"  {c.Category}: {c.Count} шт., средняя цена ≈ {c.AvgPrice:F0} Р");
Console.WriteLine("\n Поиск товаров:");
string query = "мони";
var found = products.Where(p => p.Name.Contains(query, StringComparison.OrdinalIgnoreCase));
Console.WriteLine($"По запросу \"{query}\": {string.Join(", ", found.Select(p => p.Name))}");
Console.WriteLine("\n Товары с низким остатком:");
var lowStock = products.Where(p => p.Stock < 5).OrderBy(p => p.Stock);
foreach (var p in lowStock)
Console.WriteLine($"  {p.Name} — остаток {p.Stock} шт.");
