DELIMITER $$

CREATE PROCEDURE GetSiswaRanking()
BEGIN
    WITH RankedSiswa AS (
        SELECT 
            Id, Nama, Nilai,
            RANK() OVER (ORDER BY Nilai DESC) AS RankNilai
        FROM Siswa
    )
    SELECT * FROM RankedSiswa;
END$$

DELIMITER ;