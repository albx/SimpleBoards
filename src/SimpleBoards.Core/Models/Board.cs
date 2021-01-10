using System;
using System.Collections.Generic;

namespace SimpleBoards.Core.Models
{
    public class Board
    {
        #region Properties
        public int Id { get; protected set; }

        public string Name { get; protected set; }

        public bool Deleted { get; protected set; }

        public DateTime? DeletedAt { get; protected set; }

        public DateTime CreatedAt { get; protected set; }

        public virtual ICollection<Issue> Issues { get; protected set; }
        #endregion

        #region Constructor
        protected Board() 
        { 
            Issues = new HashSet<Issue>();
        }
        #endregion

        #region Factory method
        public static Board NewBoard(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
            }

            var board = new Board
            {
                Name = name,
                CreatedAt = DateTime.UtcNow
            };

            return board;
        }
        #endregion

        #region Public methods
        public void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
            }

            Name = name;
        }

        public void Delete()
        {
            Deleted = true;
            DeletedAt = DateTime.UtcNow;
        }

        public void Restore()
        {
            Deleted = false;
            DeletedAt = null;
        }
        #endregion
    }
}