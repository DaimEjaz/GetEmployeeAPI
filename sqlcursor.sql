Declare @Count int = 0;
While @count < 10
Begin
	Set @Count = @Count +1
	Insert into Employees Values ('empName'+ CAST( @Count as varchar(6)), 'role'+ CAST( @Count as varchar(6)), 50000, 'Address'+ CAST( @Count as varchar(6)))
End

Select * from Employees;