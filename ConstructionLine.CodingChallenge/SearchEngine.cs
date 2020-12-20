using System;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;

        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts ?? throw new ArgumentNullException(nameof(shirts));
        }

        public SearchResults Search(SearchOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (options.Colors == null)
            {
                throw new ArgumentNullException(nameof(options.Colors));
            }

            if (options.Sizes == null)
            {
                throw new ArgumentNullException(nameof(options.Sizes));
            }

            var filteredShirts = _shirts.Where(shirt => (!options.Colors.Any() || options.Colors.Any(c => c.Id == shirt.Color.Id))
                                                        && (!options.Sizes.Any() || options.Sizes.Any(s => s.Id == shirt.Size.Id))).ToList();

            var searchResults = new SearchResults
            {
                Shirts = filteredShirts,
                ColorCounts = Color.All.Select(color => new ColorCount()
                {
                    Color = color,
                    Count = filteredShirts.Where(f => f.Color.Id == color.Id).Select(f => f.Color).Count()
                }).ToList(),
                SizeCounts = Size.All.Select(size => new SizeCount()
                {
                    Size = size,
                    Count = filteredShirts.Where(f => f.Size.Id == size.Id).Select(f => f.Size).Count()
                }).ToList()
            };

            return searchResults;
        }
    }
}