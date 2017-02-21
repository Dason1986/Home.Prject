create view TimeLineByYYYYMM
as
 
SELECT    TimeLine,   COUNT(0) AS count
FROM      (SELECT       SUBSTRING (AttValue ,0,5) TimeLine
                 FROM      PhotoAttribute
                 WHERE   (AttKey = 'DateTimeDigitized')) AS a1 
GROUP BY   a1.TimeLine