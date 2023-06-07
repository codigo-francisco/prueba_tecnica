CREATE PROCEDURE BorrarUsuario(
	@id int
)
AS
BEGIN
	DELETE FROM Usuarios
	WHERE id = @id
END