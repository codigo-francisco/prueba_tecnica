CREATE PROCEDURE AgregarUsuario(
	@nombre nvarchar(100),
	@apellidoPaterno nvarchar(100),
	@apellidoMaterno nvarchar(100),
	@salario money,
	@curp nvarchar(18),
	@telefono nvarchar(10)
)
AS
BEGIN
	INSERT INTO Usuarios(nombre, apellidoPaterno, apellidoMaterno, salario, curp, telefono)
	VALUES(@nombre, @apellidoPaterno, @apellidoMaterno, @salario, @curp, @telefono)
END
