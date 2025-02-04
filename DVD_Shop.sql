USE MASTER

--DROP DATABASE IN CASE ALREADY EXISTED
IF EXISTS (SELECT 1 FROM sys.databases WHERE NAME = N'DVDShop')
BEGIN
DROP DATABASE DVDShop;
END

--CREATE NEW DATABSE
CREATE DATABASE DVDShop;
--USE DATABASE AFTER CREATED
USE DVDShop;

--CREATE TABLES AND ADD CONSTRAINTS
CREATE TABLE News(
	news_id int identity(1,1) primary key,
	news_title varchar(500) not null,
	news_image varchar(100) not null,
	news_content varchar(500) not null,
	news_source varchar(50),
	publish_date date not null
)

CREATE TABLE Categories(
	category_id int primary key identity(1,1),
	category_name varchar(50) not null
)

INSERT INTO Categories(category_name) 
VALUES ('Artists'),('Albums'),('Songs'),('Games'),('Movies'),('Genres'),('Products')

CREATE TABLE Users(
	users_id int primary key identity(1,1),
	users_email varchar(50) unique not null,
	users_password char(60) not null,
	users_profile_name varchar(50) not null,
	users_date_of_birth date not null,
	users_address varchar(100) not null,
	users_phone bigint not null,
	users_activated bit not null,
	is_admin bit not null,
	is_customer bit not null,
	is_cancel bit not null,
	delete_status bit not null	
)

-- admin account: admin@gmail.com ,password = accountadmin--
INSERT INTO Users (users_email, users_password, 
					users_profile_name, users_date_of_birth, 
					users_address, users_phone, 
					users_activated, is_admin, 
					is_customer, is_cancel, delete_status)
VALUES ('admin@gmail.com', 
		'$2a$11$TLoJgGWGD0n5NVcCrJG0nuJZ0P0k0/fAAfGi.m3MUtNe54uJbPyou',
		'admin full name', '1965-04-30',
		'admin address', 588446699,
		1, 1,
		0, 0, 0);

CREATE TABLE Permission(
	permission_id int primary key identity(1,1),
	permission_type varchar(50) not null
)

CREATE TABLE AdminsPermissions(
	id int primary key identity(1,1),
	users_id int,
	permission_id int,
	constraint fk_admins foreign key(users_id) references Users(users_id) ON DELETE CASCADE,
	constraint fk_permissions foreign key(permission_id) references Permission(permission_id) ON DELETE CASCADE,
)

CREATE TABLE Suppliers(
	supplier_id int primary key identity(1,1),
	supplier_name varchar(50) not null,
	supplier_email varchar(50) not null,
	supplier_phone bigint not null,
	supplier_address varchar(100) not null,
	delete_status bit not null
)

CREATE TABLE Producers(
	producer_id int primary key identity(1,1),
	producer_name varchar(50) not null,
	producer_introduction varchar(500) not null,
	delete_status bit not null
)

CREATE TABLE Promotions(
	promotion_id int primary key identity(1,1),
	promotion_name varchar(50) not null,
	promotion_banner varchar(100),
	promotion_percent int not null,
	promotion_start_date date not null,
	promotion_expire_date date not null
)

INSERT INTO Promotions(promotion_name,promotion_banner,promotion_percent,promotion_start_date,promotion_expire_date)
VALUES  ('Basic','basic-banner.jpg',0,'2000-01-01','2030-01-01'),
		('Promotion 1', 'banner1.jpg', 10, '2024-04-09', '2024-04-15'),
		('Promotion 2', 'banner2.jpg', 20, '2024-04-10', '2024-04-20'),
		('Promotion 3', 'banner3.jpg', 15, '2024-04-11', '2024-04-25'),
		('Promotion 4', 'banner4.jpg', 25, '2024-04-12', '2024-04-30'),
		('Promotion 5', 'banner5.jpg', 30, '2024-04-13', '2024-05-05');

CREATE TABLE Genres(
	genre_id int primary key identity(1,1),
	genre_name varchar(50) not null,
	genre_description varchar(500)
)

CREATE TABLE Artists(
	artist_id int primary key identity(1,1),
	artist_name varchar(50) not null,
	artist_biography text,
	artist_photo varchar(50)
)

CREATE TABLE ArtistsGenres(
	id int primary key identity(1,1),
	artist_id int,
	genre_id int,
	constraint fk_Artists_Genres_artist foreign key(artist_id) references Artists(artist_id) ON DELETE CASCADE,
	constraint fk_Artists_Genres_genre foreign key(genre_id) references Genres(genre_id) ON DELETE CASCADE,
)

CREATE TABLE Reviews(
	review_id int primary key identity(1,1),
	review_title varchar(250) not null,
	review_content text not null,
	users_id int not null,
	constraint fk_review_user foreign key(users_id) references Users(users_id),
)

CREATE TABLE Albums(
	album_id int primary key identity(1,1),
	album_name varchar(50) not null,
	album_cover varchar(255) not null,
	producer_id int not null,
	constraint fk_album_producer foreign key(producer_id) references Producers(producer_id),
	artist_id int not null,
	constraint fk_album_artist foreign key(artist_id) references Artists(artist_id),
	album_introduction varchar(500),	
	issue_date date not null,
	category_id int not null,
	constraint fk_album_cate foreign key(category_id) references Categories(category_id),
	review_id int,
	constraint fk_album_review foreign key(review_id) references Reviews(review_id)
)

CREATE TABLE Songs(
	song_id int primary key identity(1,1),
	song_name varchar(50) not null,
	song_cover varchar(255) not null,
	song_introduction varchar(500),
	download_link varchar(60),
	producer_id int not null,
	constraint fk_song_producer foreign key(producer_id) references Producers(producer_id),
	artist_id int not null,
	constraint fk_song_artist foreign key(artist_id) references Artists(artist_id),
	category_id int not null,
	constraint fk_song_category foreign key(category_id) references Categories(category_id)
)

CREATE TABLE AlbumsSongs(
	id int primary key identity(1,1),
	album_id int,
	song_id int,	
	constraint fk_AlbumsSongs_album foreign key(album_id) references Albums(album_id) ON DELETE CASCADE,
	constraint fk_AlbumsSongs_song foreign key(song_id) references Songs(song_id) ON DELETE CASCADE
)

CREATE TABLE AlbumsGenres(
	id int primary key identity(1,1),
	album_id int,
	genre_id int,
	constraint fk_AlbumsGenres_album foreign key(album_id) references Albums(album_id) ON DELETE CASCADE,
	constraint fk_AlbumsGenres_song foreign key(genre_id) references Genres(genre_id) ON DELETE CASCADE
)

CREATE TABLE SongsGenres(
	id int primary key identity(1,1),
	song_id int,
	genre_id int,
	constraint fk_SongsGenres_song foreign key(song_id) references Songs(song_id) ON DELETE CASCADE,
	constraint fk_SongsGenres_genre foreign key(genre_id) references Genres(genre_id) ON DELETE CASCADE
)

CREATE TABLE Products(
	product_id int primary key identity(1,1),
	product_title varchar(255) not null,
	product_price float,
	product_quantity int not null,
	sold_unit int not null,
	product_rate int,
	created_at date not null,
	is_delete bit not null,
	supplier_id int not null,
	constraint fk_product_supplier foreign key (supplier_id) references Suppliers(supplier_id),
	album_id int,
	constraint fk_product_album foreign key (album_id) references Albums(album_id),	
	promotion_id int not null,
	constraint fk_product_promotion foreign key(promotion_id) references Promotions(promotion_id)
)

CREATE TABLE Ratings(
	rating_id int primary key identity(1,1),
	product_id int,
	constraint fk_rating_product foreign key(product_id) references Products(product_id),
	users_id int,
	constraint fk_rating_user foreign key(users_id) references Users(users_id),
	rating int,
	comment varchar(250)
)

CREATE TABLE Carts(
	cart_id int primary key identity(1,1),
	users_id int not null,
	quantity int not null,
	price float not null,
	constraint fk_cart_user foreign key(users_id) references Users(users_id),
	product_id int not null,
	constraint fk_cart_product foreign key(product_id) references Products(product_id),
)

CREATE TABLE Orders(
	order_id int primary key identity(1,1),
	users_id int not null,
	constraint fk_order_user foreign key(users_id) references Users(users_id),
	order_date date not null,
	order_status bit not null,
	total_amount float not null,
	ship_address varchar(255) not null,
	note varchar(255),
)

CREATE TABLE Invoice(
	invoice_id int primary key identity(1,1),
	order_id int,
	constraint fk_invoice_order foreign key(order_id) references Orders(order_id),
	invoice_date date not null,
	total_amount float not null,
)

CREATE TABLE InvoiceDetail(
	detail_id int primary key identity(1,1),
	invoice_id int,
	constraint fk_detail_invoice foreign key(invoice_id) references Invoice(invoice_id),
	order_id int,
	constraint fk_detail_order foreign key(order_id) references Orders(order_id),
	product_id int,
	constraint fk_detail_product foreign key(product_id) references Products(product_id),
	quantity int,
	price_after_promotion float(11),
	total_amount float(11)
)

CREATE TABLE Feedbacks(
	feedback_id int primary key identity(1,1),
	feedback_content varchar(500) not null,
	feedback_reply varchar(500),
	feedback_date date not null,
	users_id int not null,
	constraint fk_feedback_member foreign key(users_id) references Users(users_id)
)

CREATE TABLE Games(
	game_id int primary key identity(1,1),
	game_title char(50) not null,
	game_cover varchar(255) not null,
	game_description varchar(500) not null,
	game_trailer varchar(500) not null,
	download_link varchar(60),
	producer_id int not null,
	constraint fk_game_producer foreign key (producer_id) references Producers(producer_id),
	category_id int not null,
	constraint fk_game_category foreign key(category_id) references Categories(category_id)
)

CREATE TABLE GamesGenres(
	id int primary key identity(1,1),
	game_id int not null,
	genre_id int not null,
	constraint fk_gg_game foreign key(game_id) references Games(game_id) ON DELETE CASCADE,
	constraint fk_gg_genre foreign key(genre_id) references Genres(genre_id) ON DELETE CASCADE
)

CREATE TABLE Movies(
	movie_id int primary key identity(1,1),
	movie_title char(50) not null,
	movie_cover varchar(255) not null,
	movie_description varchar(500) not null,
	movie_trailer varchar(500) not null,
	download_link varchar(60),
	producer_id int not null,
	constraint fk_movie_producer foreign key (producer_id) references Producers(producer_id),
	category_id int not null,
	constraint fk_movie_category foreign key(category_id) references Categories(category_id)
)

CREATE TABLE MoviesGenres(
	id int primary key identity(1,1),
	movie_id int not null,
	genre_id int not null,
	constraint fk_mg_game foreign key(movie_id) references Movies(movie_id) ON DELETE CASCADE,
	constraint fk_mg_genre foreign key(genre_id) references Genres(genre_id) ON DELETE CASCADE
)

--INSERT BASIC INFO
-- Inserting records for songs
INSERT INTO Suppliers (supplier_name, supplier_email, supplier_phone, supplier_address, delete_status)
VALUES
('Universal Music Group', 'contact@umusic.com', 1234567890, '100 Universal City Plaza, Universal City, CA', 0),
('Sony Music Entertainment', 'info@sonymusic.com', 2345678901, '25 Madison Ave, New York, NY', 0),
('Warner Music Group', 'info@warnermusic.com', 3456789012, '1633 Broadway, New York, NY', 0),
('Electronic Arts', 'info@ea.com', 7890123456, '209 Redwood Shores Parkway, Redwood City, CA', 0),
('Activision Blizzard', 'info@activision.com', 8901234567, '3100 Ocean Park Blvd, Santa Monica, CA', 0),
('Ubisoft', 'info@ubisoft.com', 9012345678, '28 Rue Armand Carrel, Montreuil, France', 0),
('Walt Disney Studios', 'info@disney.com', 4567890123, '500 S. Buena Vista Street, Burbank, CA', 0),
('Warner Bros. Pictures', 'info@warnerbros.com', 5678901234, '4000 Warner Blvd, Burbank, CA', 0),
('Universal Pictures', 'info@universalpictures.com', 6789012345, '100 Universal City Plaza, Universal City, CA', 0);

INSERT INTO Producers (producer_name, producer_introduction, delete_status)
VALUES
('Max Martin', 'Swedish record producer and songwriter known for producing numerous hit songs.', 0),
('Dr. Luke', 'American record producer and songwriter known for his work in pop music.', 0),
('Pharrell Williams', 'American singer, songwriter, and record producer known for his versatile music career.', 0),
('Hikaru Utada', 'Japanese-American singer-songwriter known for her contributions to the J-pop genre.', 0),
('Yoko Kanno', 'Japanese composer and musician known for her work in anime soundtracks and video game music.', 0),
('Hideo Kojima', 'Japanese video game designer, director, and producer known for creating the Metal Gear series.', 0),
('Shigeru Miyamoto', 'Japanese video game designer and producer known for creating iconic game franchises like Mario and Zelda.', 0),
('Todd Howard', 'American video game designer, director, and producer known for his work on open-world RPGs.', 0),
('Tim Schafer', 'American video game designer, programmer, and founder of Double Fine Productions, known for classic adventure games like Grim Fandango and Psychonauts.', 0),
('Jenova Chen', 'Chinese-American video game designer and co-founder of thatgamecompany, known for creating emotionally engaging games like Journey and Flower.', 0),
('Kevin Feige', 'President of Marvel Studios known for producing successful superhero films.', 0),
('Kathleen Kennedy', 'Film producer known for her work on various iconic film franchises.', 0),
('Jerry Bruckheimer', 'American film and television producer known for producing blockbuster action films.', 0),
('Steven Spielberg', 'American filmmaker known for directing blockbuster films such as Jurassic Park and Saving Private Ryan.', 0),
('Christopher Nolan', 'British-American filmmaker renowned for his intricate and mind-bending films like Inception and The Dark Knight trilogy.', 0);

INSERT INTO Genres (genre_name, genre_description)
VALUES
('Pop', 'Popular music genre characterized by catchy melodies and rhythmic patterns.'),
('Rock', 'Genre of popular music that originated as "rock and roll" in the 1950s.'),
('Hip Hop', 'Genre of music characterized by rhythmic speech and beats.'),
('R&B', 'Rhythm and blues music genre featuring soulful vocals and strong rhythms.'),
('Electronic', 'Genre of music produced using electronic devices and technology.'),
('Action', 'Genre of video games featuring fast-paced gameplay and combat.'),
('Adventure', 'Genre of video games emphasizing exploration and puzzle-solving.'),
('Role-Playing', 'Genre of video games where players assume the roles of characters in fictional worlds.'),
('Strategy', 'Genre of video games requiring strategic planning and decision-making.'),
('Simulation', 'Genre of video games simulating real-world activities or scenarios.'),
('Action', 'Genre of film characterized by intense physical activity and violence.'),
('Drama', 'Genre of film focusing on emotional and interpersonal conflicts.'),
('Comedy', 'Genre of film intended to provoke laughter and entertain.'),
('Thriller', 'Genre of film designed to evoke excitement, tension, and suspense.'),
('Horror', 'Genre of film intended to frighten, scare, or disgust the viewer.');

-- Inserting records for artists with associated genres for songs
INSERT INTO Artists (artist_name, artist_biography, artist_photo)
VALUES
('Adele', 'English singer-songwriter known for her soulful vocals and emotional ballads.', 'adele.jpg'),
('Ed Sheeran', 'British singer-songwriter known for his heartfelt lyrics and acoustic pop songs.', 'ed_sheeran.jpg'),
('Beyoncé', 'American singer, songwriter, and actress known for her powerful vocals and dynamic performances.', 'beyonce.jpg'),
('Justin Bieber', 'Canadian singer-songwriter known for his pop and R&B hits.', 'justin_bieber.jpg'),
('Taylor Swift', 'American singer-songwriter known for her storytelling and country-pop music.', 'taylor_swift.jpg');


-- Inserting records into ArtistsGenres to associate artists with genres for songs
INSERT INTO ArtistsGenres (artist_id, genre_id)
VALUES  (1, 1), (1, 2),(1, 3),
		(2, 1), (2, 4),
		(3, 1), (3, 3), (3, 5),
		(4, 1), (4, 3),
		(5, 1), (5, 4);

INSERT INTO Songs (song_name, song_introduction, song_cover, download_link, producer_id, artist_id, category_id)
VALUES
('Hello', 'Emotional ballad', 'hello.jpg', 'https://www.example.com/hello.mp3', 1, 1, 3),
('Rolling in the Deep', 'Powerful anthem', 'rolling_in_the_deep.jpg', 'https://www.example.com/rolling_in_the_deep.mp3', 1, 1, 3),
('Someone Like You', 'Heartfelt ballad', 'someone_like_you.jpg', 'https://www.example.com/someone_like_you.mp3', 1, 1, 3),
('Set Fire to the Rain', 'Soulful track', 'set_fire_to_the_rain.jpg', 'https://www.example.com/set_fire_to_the_rain.mp3', 1, 1, 3),
('Shape of You', 'Catchy pop tune', 'shape_of_you.jpg', 'https://www.example.com/shape_of_you.mp3', 2, 2, 3),
('Thinking Out Loud', 'Romantic ballad', 'thinking_out_loud.jpg', 'https://www.example.com/thinking_out_loud.mp3', 2, 2, 3),
('Castle on the Hill', 'Nostalgic anthem', 'castle_on_the_hill.jpg', 'https://www.example.com/castle_on_the_hill.mp3', 2, 2, 3),
('Photograph', 'Melancholic track', 'photograph.jpg', 'https://www.example.com/photograph.mp3', 2, 2, 3),
('Single Ladies (Put a Ring on It)', 'Empowering anthem', 'single_ladies.jpg', 'https://www.example.com/single_ladies.mp3', 3, 3, 3),
('Halo', 'Soulful ballad', 'halo.jpg', 'https://www.example.com/halo.mp3', 3, 3, 3),
('Crazy in Love', 'Iconic hit', 'crazy_in_love.jpg', 'https://www.example.com/crazy_in_love.mp3', 3, 3, 3),
('Irreplaceable', 'Breakup anthem', 'irreplaceable.jpg', 'https://www.example.com/irreplaceable.mp3', 3, 3, 3),
('Baby', 'Breakout hit', 'baby.jpg', 'https://www.example.com/baby.mp3', 4, 4, 3),
('Sorry', 'Apology anthem', 'sorry.jpg', 'https://www.example.com/sorry.mp3', 4, 4, 3),
('Love Yourself', 'Acoustic ballad', 'love_yourself.jpg', 'https://www.example.com/love_yourself.mp3', 4, 4, 3),
('Yummy', 'Infectious groove', 'yummy.jpg', 'https://www.example.com/yummy.mp3', 4, 4, 3),
('Love Story', 'Country-pop crossover', 'love_story.jpg', 'https://www.example.com/love_story.mp3', 5, 5, 3),
('Shake It Off', 'Catchy pop anthem', 'shake_it_off.jpg', 'https://www.example.com/shake_it_off.mp3', 5, 5, 3),
('Blank Space', 'Chart-topping hit', 'blank_space.jpg', 'https://www.example.com/blank_space.mp3', 5, 5, 3),
('You Belong with Me', 'Teenage love song', 'you_belong_with_me.jpg', 'https://www.example.com/you_belong_with_me.mp3', 5, 5, 3);

INSERT INTO SongsGenres (song_id, genre_id)
VALUES
(1, 1),(1, 4),
(2, 1),(2, 2),
(3, 4),(3, 3),
(4, 4),(4, 2),
(5, 1),(5, 5),
(6, 3),(6, 2),
(7, 1),(7, 2),
(8, 4),(8, 1),
(9, 4),(9, 1),
(10, 4),(10, 1),
(11, 4),(11, 1),
(12, 4),(12, 1),
(13, 5),(13, 4),
(14, 5),(14, 4),
(15, 5),(15, 4),
(16, 5),(16, 4),
(17, 2),(17, 1),
(18, 2),(18, 1),
(19, 2),(19, 1),
(20, 2),(20, 1);

INSERT INTO Albums (album_name, album_cover, producer_id, artist_id, album_introduction, issue_date, category_id)
VALUES
('25', '25.jpg', 1, 1, 'Adele''s third studio album', '2015-11-20', 3),
('Divide', 'divide.jpg', 2, 2, 'Ed Sheeran''s third studio album', '2017-03-03', 3),
('Lemonade', 'lemonade.jpg', 3, 3, 'Beyoncé''s sixth studio album', '2016-04-23', 3),
('Purpose', 'purpose.jpg', 4, 4, 'Justin Bieber''s fourth studio album', '2015-11-13', 3),
('1989', '1989.jpg', 5, 5, 'Taylor Swift''s fifth studio album', '2014-10-27', 3);

-- Insert values into AlbumsGenres table
INSERT INTO AlbumsGenres (album_id, genre_id)
VALUES
(1, 1),(1, 4),
(2, 1),(2, 2),
(3, 1),(3, 4),(3, 5),
(4, 1),(4, 3),
(5, 1),(5, 2);

-- Insert values into AlbumsSongs table
INSERT INTO AlbumsSongs (album_id, song_id)
VALUES
(1, 1),(1, 2),(1, 3),(1, 4),
(2, 5),(2, 6),(2, 7),(2, 8),
(3, 9),(3, 10),(3, 11),(3, 12),
(4, 13),(4, 14),(4, 15),(4, 16),
(5, 17),(5, 18),(5, 19),(5, 20);

INSERT INTO Games (game_title, game_cover, game_description, game_trailer, download_link, producer_id, category_id)
VALUES
('The Last of Us Part II', 'last_of_us_2.jpg', 'Action-adventure game set in a post-apocalyptic world.', 'https://www.youtube.com/watch?v=qPNiIeKMHyg', 'https://www.example.com/last_of_us_2.zip', 6, 4),
('Call of Duty: Warzone', 'cod_warzone.jpg', 'Battle royale game set in the Call of Duty universe.', 'https://www.youtube.com/watch?v=Ujyk04VHh8Y', 'https://www.example.com/cod_warzone.zip', 7, 4),
('Assassins Creed Valhalla', 'ac_valhalla.jpg', 'Open-world action-adventure game set in the Viking Age.', 'https://www.youtube.com/watch?v=5SP9gIB_fy4', 'https://www.example.com/ac_valhalla.zip', 8, 4),
('Cyberpunk 2077', 'cyberpunk_2077.jpg', 'Open-world RPG set in a dystopian future.', 'https://www.youtube.com/watch?v=LembwKDo1Dk', 'https://www.example.com/cyberpunk_2077.zip', 9, 4),
('Among Us', 'among_us.jpg', 'Online multiplayer social deduction game.', 'https://www.youtube.com/watch?v=rB4v-vX7jCE', 'https://www.example.com/among_us.zip', 10, 4),
('Red Dead Redemption 2', 'red_dead_redemption_2.jpg', 'Open-world Western action-adventure game.', 'https://www.youtube.com/watch?v=gmA6MrX81z4', 'https://www.example.com/red_dead_redemption_2.zip', 6, 4),
('The Legend of Zelda: Breath of the Wild', 'zelda_breath_of_the_wild.jpg', 'Action-adventure game set in a vast open world.', 'https://www.youtube.com/watch?v=zw47_q9wbBE', 'https://www.example.com/zelda_breath_of_the_wild.zip', 7, 4),
('Dark Souls III', 'dark_souls_3.jpg', 'Challenging action RPG known for its difficulty.', 'https://www.youtube.com/watch?v=JKwSvAeAVsE', 'https://www.example.com/dark_souls_3.zip', 8, 4),
('Stardew Valley', 'stardew_valley.jpg', 'Farming simulation RPG with sandbox elements.', 'https://www.youtube.com/watch?v=ot7uXNQskhs', 'https://www.example.com/stardew_valley.zip', 9, 4),
('Minecraft', 'minecraft.jpg', 'Sandbox game with blocky graphics and endless possibilities.', 'https://www.youtube.com/watch?v=MmB9b5njVbA', 'https://www.example.com/minecraft.zip', 10, 4);

INSERT INTO GamesGenres (game_id, genre_id)
VALUES
(1, 6), (1, 7),
(2, 6), (2, 10), 
(3, 6), (3, 7), 
(4, 6), (4, 8),
(5, 10), (6, 6), (6, 7),
(7, 7), (7, 8),
(8, 6), (8, 8),
(9, 7), (9, 9),
(10, 10), (10, 9);

INSERT INTO Movies(movie_title, movie_cover, movie_description, movie_trailer, download_link, producer_id, category_id)
VALUES
('Avengers: Endgame', 'avengers_endgame.jpg', 'The Avengers assemble one last time to undo the devastating effects of Thanos'' snap and restore balance to the universe.', 'https://www.example.com/avengers_endgame_trailer', 'https://www.example.com/avengers_endgame_download', 11, 5),
('Jurassic Park', 'jurassic_park.jpg', 'A theme park featuring genetically engineered dinosaurs turns into a nightmare when the creatures break free and go on a rampage.', 'https://www.example.com/jurassic_park_trailer', 'https://www.example.com/jurassic_park_download', 14, 5),
('Toy Story', 'toy_story.jpg', 'A group of toys come to life and go on adventures when their owner isn''t around.', 'https://www.example.com/toy_story_trailer', 'https://www.example.com/toy_story_download', 11, 5),
('The Dark Knight', 'dark_knight.jpg', 'Batman faces off against the Joker, a criminal mastermind wreaking havoc on Gotham City.', 'https://www.example.com/dark_knight_trailer', 'https://www.example.com/dark_knight_download', 14, 5),
('Inception', 'inception.jpg', 'A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a CEO.', 'https://www.example.com/inception_trailer', 'https://www.example.com/inception_download', 15, 5),
('Finding Nemo', 'finding_nemo.jpg', 'A clownfish embarks on a journey to find his son, who has been captured by a diver.', 'https://www.example.com/finding_nemo_trailer', 'https://www.example.com/finding_nemo_download', 11, 5),
('The Lion King', 'lion_king.jpg', 'A young lion prince flees his kingdom only to learn the true meaning of responsibility and bravery.', 'https://www.example.com/lion_king_trailer', 'https://www.example.com/lion_king_download', 11, 5),
('Avatar', 'avatar.jpg', 'A paraplegic Marine dispatched to the moon Pandora on a unique mission becomes torn between following his orders and protecting the world he feels is his home.', 'https://www.example.com/avatar_trailer', 'https://www.example.com/avatar_download', 13, 5),
('The Shawshank Redemption', 'shawshank_redemption.jpg', 'Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.', 'https://www.example.com/shawshank_redemption_trailer', 'https://www.example.com/shawshank_redemption_download', 14, 5),
('Forrest Gump', 'forrest_gump.jpg', 'The presidencies of Kennedy and Johnson, the events of Vietnam, Watergate, and other historical events unfold from the perspective of an Alabama man with an IQ of 75, whose only desire is to be reunited with his childhood sweetheart.', 'https://www.example.com/forrest_gump_trailer', 'https://www.example.com/forrest_gump_download', 14, 5);

INSERT INTO MoviesGenres (movie_id, genre_id) 
VALUES (1, 11),(1, 14), 
(2, 11),(2, 15),(3, 12), 
(4, 11),(4, 14),(5, 14), 
(6, 12),(6, 15), 
(7, 12),(7, 15), 
(8, 11),(8, 12), 
(9, 12),(9, 14), 
(10, 12),(10, 13);

-- Insert records for products (albums)
INSERT INTO Products (product_title, product_price, product_quantity, sold_unit, product_rate, created_at, supplier_id, album_id, promotion_id,is_delete)
VALUES
('Product 1 Title', 15.99, 100, 50, 4, '2024-04-01', 1, 1, 1,0),
('Product 2 Title', 12.99, 150, 75, 4, '2024-04-05', 2, 2, 2,0),
('Product 3 Title', 17.99, 120, 60, 4, '2024-04-10', 3, 3, 3,0),
('Product 4 Title', 14.99, 90, 45, 4, '2024-04-15', 4, 4, 2,0),
('Product 5 Title', 16.99, 110, 55, 4, '2024-04-20', 5, 5, 4,0);
