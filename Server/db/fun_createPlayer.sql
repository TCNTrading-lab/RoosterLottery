USE [RoosterLottery]
GO
/****** Object:  StoredProcedure [dbo].[CreatePlayer]    Script Date: 5/29/2024 4:17:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[CreatePlayer]
    @FullName NVARCHAR(100),
    @DateOfBirth DATE,
    @PhoneNumber NVARCHAR(15)
AS
BEGIN
    -- Ensure no SQL injection
    SET @FullName = LTRIM(RTRIM(@FullName))
    SET @PhoneNumber = LTRIM(RTRIM(@PhoneNumber))

    -- Check if the phone number already exists
    IF NOT EXISTS (SELECT 1 FROM PLAYER WHERE PhoneNumber = @PhoneNumber)
    BEGIN
        -- Insert the new player into the PLAYER table
        INSERT INTO PLAYER (FullName, DateOfBirth, PhoneNumber)
        VALUES (@FullName, @DateOfBirth, @PhoneNumber)

        -- Return the ID of the newly created player
        SELECT SCOPE_IDENTITY() AS NewPlayerID
    END
    ELSE
    BEGIN
        -- Return a message indicating the phone number already exists
        SELECT 'PhoneNumberExists' AS Message
    END
END