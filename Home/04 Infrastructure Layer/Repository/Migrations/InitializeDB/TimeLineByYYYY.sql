create view TimeLineByYYYY
as
 
 SELECT    TimeLine,   COUNT(0) AS count
FROM      (SELECT       left (AttValue , 11) TimeLine
                 FROM      PhotoAttribute
                 WHERE   (AttKey = 'DateTimeDigitized')) AS a1 
GROUP BY   a1.TimeLine