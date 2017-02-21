create view TimeLineByYYYY
as
 
SELECT    TimeLine,   COUNT(0)   'Count'
FROM      (SELECT       SUBSTRING (AttValue ,0,5) TimeLine
                 FROM      PhotoAttribute
                 WHERE   (AttKey = 'DateTimeDigitized' and AttValue !='unknown')) AS a1 
GROUP BY   a1.TimeLine
Union 
select 'unknown'   TimeLine, count(0)   'Count'
                 FROM      PhotoAttribute
                 WHERE   (AttKey = 'DateTimeDigitized' and AttValue='unknown')

