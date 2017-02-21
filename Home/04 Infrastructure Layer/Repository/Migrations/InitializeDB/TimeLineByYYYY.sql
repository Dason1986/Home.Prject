create OR REPLACE view TimeLineByYYYY
as
 
SELECT  left(AttValue ,4) TimeLine ,count(0) 'Count'
                 FROM      PhotoAttribute
                 WHERE   (AttKey = 'DateTimeDigitized' and AttValue !='unknown')
Union 
select 'unknown'   TimeLine, count(0)   'Count'
                 FROM      PhotoAttribute
                 WHERE   (AttKey = 'DateTimeDigitized' and AttValue='unknown')