 
   
   create   VIEW [duplicatebymd5view] AS
    SELECT 
        [fileinfo].[MD5] AS md5 , [filesize], COUNT(0) AS count
    FROM
        [fileinfo]
    WHERE
        ([fileinfo].[StatusCode] = 2)
    GROUP BY [fileinfo].[MD5],[fileinfo].[filesize]
    HAVING (COUNT([fileinfo].[MD5]) > 1)