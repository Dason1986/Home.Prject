create OR REPLACE view TimeLineByYYYYMMDD
as
 
SELECT  left(AttValue ,10) TimeLine ,count(0) 'Count'
                 FROM      PhotoAttribute
                 WHERE   (AttKey = 'DateTimeDigitized' and AttValue !='unknown')
Union 
select 'unknown'   TimeLine, count(0)   'Count'
                 FROM      PhotoAttribute
                 WHERE   (AttKey = 'DateTimeDigitized' and AttValue='unknown')