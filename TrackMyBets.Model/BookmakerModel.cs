using System.ComponentModel.DataAnnotations;
using TrackMyBets.Business.Entities;
using TrackMyBets.Data.Models;

namespace TrackMyBets.Model
{
    public class BookmakerModel
    {
        #region DBContext
        private static BD_TRACKMYBETSContext _dbContext;
        #endregion

        #region Constructor
        public BookmakerModel(BD_TRACKMYBETSContext dbCOntext)
        {
            _dbContext = dbCOntext;
        }
        #endregion

        #region Properties
        public int IdBookmaker { get; set; }

        [Required]
        public string DescBookmaker { get; set; }
        #endregion

        #region Public Methods
        public static BookmakerModel FromEntity(BookmakerEntity bookmaker)
        {
            BookmakerModel bookmakerModel = new BookmakerModel(_dbContext)
            {
                IdBookmaker = bookmaker.IdBookmaker,
                DescBookmaker = bookmaker.DescBookmaker
            };

            return bookmakerModel;
        }

        public BookmakerEntity ToEntities()
        {
            var bookmakerEntity = new BookmakerEntity(_dbContext)
            {
                IdBookmaker = IdBookmaker,
                DescBookmaker = DescBookmaker
            };

            return bookmakerEntity;
        }
        #endregion
    }
}
