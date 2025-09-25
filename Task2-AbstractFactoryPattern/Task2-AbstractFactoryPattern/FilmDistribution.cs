using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2_AbstractFactoryPattern
{
    internal class FilmDistribution
    {
        //Абстрактный класс аудиодорожки
        abstract class AudioTrack
        {
            public abstract string GetAudioInfo();
        }

        //Абстрактный класс Субтитров
        abstract class Subtitles
        {
            public abstract string GetSubtitleInfo();
        }

        //Класс русская аудиодорожка
        class RussianAudio : AudioTrack
        {
            public override string GetAudioInfo() => "Аудиодорожка: Русский";
        }

        //Класс русские субтитры
        class RussianSubtitles : Subtitles
        {
            public override string GetSubtitleInfo() => "Субтитры: Русский";
        }

        //Класс английская аудиодорожка
        class EnglishAudio : AudioTrack
        {
            public override string GetAudioInfo() => "Audio Track: English";
        }

        //Класс английские субтитры
        class EnglishSubtitles : Subtitles
        {
            public override string GetSubtitleInfo() => "Subtitles: English";
        }

        //Класс абстрактной фабрики
        abstract class MovieFactory
        {
            public abstract AudioTrack CreateAudioTrack();
            public abstract Subtitles CreateSubtitles();
        }

        //Фабрика создания фильма с русской аудиодорожкой и русскими субтитрами
        class RussianMovieFactory : MovieFactory
        {
            public override AudioTrack CreateAudioTrack() => new RussianAudio();
            public override Subtitles CreateSubtitles() => new RussianSubtitles();
        }

        //Фабрика создания фильма с английской аудиодорожкой и английскими субтитрами
        class EnglishMovieFactory : MovieFactory
        {
            public override AudioTrack CreateAudioTrack() => new EnglishAudio();
            public override Subtitles CreateSubtitles() => new EnglishSubtitles();
        }

        //Клиент - сам фильм
        class Movie
        {
            private AudioTrack audio_track;
            private Subtitles subtitles;
            public Movie(MovieFactory factory)
            {
                audio_track = factory.CreateAudioTrack();
                subtitles = factory.CreateSubtitles();
            }
            public void ShowInfo()
            {
                Console.WriteLine(audio_track.GetAudioInfo());
                Console.WriteLine(subtitles.GetSubtitleInfo());
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в систему Кинопрокат!");
            Console.WriteLine("Выберите язык: 1 - Русский, 2 - Английский");

            string choice = Console.ReadLine();
            MovieFactory factory;

            if (choice == "1")
                factory = new RussianMovieFactory();
            else if (choice == "2")
                factory = new EnglishMovieFactory();
            else
            {
                Console.WriteLine("Неверный ввод!");
                return;
            }

            Movie movie = new Movie(factory);

            Console.WriteLine("\nВаш фильм готов:");
            movie.ShowInfo();
        }
    }
}
