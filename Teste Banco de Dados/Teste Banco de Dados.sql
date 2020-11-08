
/*A*/ INSERT INTO Store (stor_id, stor_name) VALUES (1, 'Loja 1');
/*B*/ UPDATE Titles SET type = 'culinária' WHERE title_id = 'MC3021';
/*C*/ DELETE FROM Sales WHERE stor_id = '7066';
/*D*/ SELECT Store.stor_id FROM Store LEFT JOIN Sales ON Store.stor_id = Sales.stor_id WHERE Sales.stor_id IS NULL;
/*E*/ SELECT Titles.title_id FROM Titles LEFT JOIN Sales ON Titles.title_id = Sales.title_id WHERE Sales.title_id IS NULL;
/*F*/ SELECT Titles.title_id, Titles.title, Titles.type, SUM(ISNULL(qty,0)) as sold FROM Titles LEFT JOIN Sales ON Titles.title_id = Sales.title_id GROUP BY Titles.title_id,Titles.title, Titles.type;
/*G*/ SELECT title, COUNT(title) FROM Titles GROUP BY title HAVING COUNT(title) > 2;