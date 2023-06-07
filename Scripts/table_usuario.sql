create table Usuarios(
	id int primary key identity(1,1),
	nombre nvarchar(100),
	apellidoPaterno nvarchar(100),
	apellidoMaterno nvarchar(100),
	salario money,
	curp nvarchar(18) unique,
	telefono nvarchar(10)
)