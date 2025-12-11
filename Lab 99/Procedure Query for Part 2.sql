CREATE OR ALTER PROCEDURE purchaseTicket
    @EntreeId  TINYINT,
    @SideId    TINYINT,
    @StudentId INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;

    DECLARE @TicketId      INT;
    DECLARE @EntreePrice   FLOAT;
    DECLARE @SidePrice     FLOAT;
    DECLARE @TotalPrice    FLOAT;
    DECLARE @StudentBucks  FLOAT;
    DECLARE @EntreeInv     TINYINT;
    DECLARE @SideInv       TINYINT;

    SET @TicketId = NEXT VALUE FOR TicketIDSeq;

    SELECT
        @EntreePrice = Price,
        @EntreeInv   = InventoryCount
    FROM kayleb.Product
    WHERE ProductID = @EntreeId;

    SELECT
        @SidePrice = Price,
        @SideInv   = InventoryCount
    FROM kayleb.Product
    WHERE ProductID = @SideId;

    SELECT
        @StudentBucks = owlBucks
    FROM kayleb.Student
    WHERE StudentID = @StudentId;

    SET @TotalPrice = @EntreePrice + @SidePrice;

    IF (@EntreeInv IS NULL OR @EntreeInv <= 0)
    BEGIN
        ROLLBACK TRANSACTION;
        RAISERROR('Entree is out of stock.', 16, 1);
        RETURN;
    END;

    IF (@SideInv IS NULL OR @SideInv <= 0)
    BEGIN
        ROLLBACK TRANSACTION;
        RAISERROR('Side is out of stock.', 16, 1);
        RETURN;
    END;

    IF (@StudentBucks IS NULL OR @StudentBucks < @TotalPrice)
    BEGIN
        ROLLBACK TRANSACTION;
        RAISERROR('Student does not have enough owlBucks.', 16, 1);
        RETURN;
    END;

    INSERT INTO kayleb.Ticket (TicketID, TicketTime, StudentID, EntreeId, SideId)
    VALUES (@TicketId, GETDATE(), @StudentId, @EntreeId, @SideId);

    UPDATE kayleb.Product
    SET InventoryCount = InventoryCount - 1
    WHERE ProductID = @EntreeId;

    UPDATE kayleb.Product
    SET InventoryCount = InventoryCount - 1
    WHERE ProductID = @SideId;

    UPDATE kayleb.Student
    SET owlBucks = owlBucks - @TotalPrice
    WHERE StudentID = @StudentId;

    COMMIT TRANSACTION;
END;
GO
