var gulp = require('gulp');
var sass = require('gulp-sass');
gulp.task('sass', function () {
    return gulp.src('scss/style.scss')
    .pipe(sass()) // Converts Sass to CSS with gulp-sass
    .pipe(gulp.dest('css/'))
}); // slut tag


// beder Gulp om at holde øje med Sass filer
// gulp watch syntax for flere filer på samme tid 
gulp.task('watch', function () {
    gulp.watch('app/scss/**/*.scss', ['sass']);
});



