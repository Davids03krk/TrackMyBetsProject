using System.ComponentModel.DataAnnotations;
using TrackMyBets.Business.Entities;
using TrackMyBets.Data.Models;

namespace TrackMyBets.Model
{
    public class BookmakerModel
    {
        #region Properties
        public int IdBookmaker { get; set; }

        [Required]
        public string DescBookmaker { get; set; }
        #endregion

        #region Public Methods
        public static BookmakerModel FromEntity(BookmakerEntity bookmaker)
        {
            BookmakerModel bookmakerModel = new BookmakerModel()
            {
                IdBookmaker = bookmaker.IdBookmaker,
                DescBookmaker = bookmaker.DescBookmaker
            };

            return bookmakerModel;
        }

        public BookmakerEntity ToEntities()
        {
            var bookmakerEntity = new BookmakerEntity()
            {
                IdBookmaker = IdBookmaker,
                DescBookmaker = DescBookmaker
            };

            return bookmakerEntity;
        }
        #endregion
    }
}
