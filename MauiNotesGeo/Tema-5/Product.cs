using CatalogApp;
List<Product> products =
[
new("Клавиатура", "Периферия", 1200m, 15),
new("Мышь",       "Периферия", 800m,  0),
new("Монитор",    "Мониторы",  18000m, 4),
new("SSD 1TB",    "Накопители", 7500m, 9),
new("Коврик",     "Периферия", 300m,  40),
];
Console.WriteLine("=== Каталог товаров ===\n");
Console.WriteLine($"Всего товаров: {products.Count}");
