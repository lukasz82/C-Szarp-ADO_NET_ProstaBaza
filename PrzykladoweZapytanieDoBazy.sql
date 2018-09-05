Select Customers.FirstName, Customers.LastName, Inventory.PetName From Customers 
Join Orders On Orders.CustId = Customers.CustId
Join Inventory On Orders.CarId = Inventory.CarId where Inventory.PetName = 'Punto'