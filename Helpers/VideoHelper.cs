using HelloWorldMVC.Models;

namespace HelloWorldMVC.Helpers
{
    public static class VideoHelper
    {
        public static Video getVideoWithIdParsed(Video video)
        {
            video.Url = $"https://www.youtube.com/embed/{video.Url.Split('=')[1]}";
            return video;
        }
    }
}