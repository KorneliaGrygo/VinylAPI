using FluentValidation;
using System;
using VinylAPI.Models;

namespace VinylAPI.Validators
{
    public class CreateAlbumDtoValidator : AbstractValidator<CreateAlbumDto> // abstract alt + enter
    {
        public CreateAlbumDtoValidator() //ctor
        {
            RuleFor(album => album.Name).NotEmpty().MaximumLength(200);
            RuleFor(album => album.AmountOfSongs).NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(album => album.PublicationYear).NotEmpty().LessThanOrEqualTo(DateTime.Now.Year);
            RuleFor(album => album.Genre).MaximumLength(50);
        }
    }
}
