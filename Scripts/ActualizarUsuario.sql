CREATE PROCEDURE ModificarUsuario(
	@id int,
	@nombre nvarchar(100),
	@apellidoPaterno nvarchar(100),
	@apellidoMaterno nvarchar(100),
	@salario money,
	@curp nvarchar(18),
	@telefono nvarchar(10)
)
AS
BEGIN
	UPDATE Usuarios
	SET nombre = @nombre,
	apellidoPaterno = @apellidoPaterno,
	apellidoMaterno = @apellidoMaterno,
	salario = @salario,
	curp = @curp,
	telefono = @telefono
	where id = @id
END