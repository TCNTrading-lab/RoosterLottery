CREATE PROCEDURE CreatePlayer
    @FullName NVARCHAR(100),
    @DateOfBirth DATE,
    @PhoneNumber NVARCHAR(15)
AS
BEGIN
    -- Ensure no SQL injection
    SET @FullName = LTRIM(RTRIM(@FullName))
    SET @PhoneNumber = LTRIM(RTRIM(@PhoneNumber))

    -- Insert the new player into the PLAYER table
    INSERT INTO PLAYER (FullName, DateOfBirth, PhoneNumber)
    VALUES (@FullName, @DateOfBirth, @PhoneNumber)

    -- Return the ID of the newly created player
    SELECT SCOPE_IDENTITY() AS NewPlayerID
END
GO
