using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCheckerApp.Utils.FileFormats
{
    public static class FileFormats
    {
        public static readonly Dictionary<FileCategory, List<string>> Formats = new()
        {
            //{ FileCategory.All, new List<string>()
            //    {
            //        "*.*"
            //    }
            //},
            { FileCategory.Office, new List<string>
                {
                    "*.doc", "*.docx", "*.odt", "*.rtf", "*.txt", "*.csv", "*.tsv", "*.xls", "*.xlsx",
                    "*.ods", "*.ppt", "*.pptx", "*.wpd", "*.pages", "*.numbers", "*.key", "*.xltx", "*.potx",
                    "*.dotx", "*.rtf", "*.csv", "*.dbf", "*.xml", "*.odm", "*.odb", "*.fods", "*.ots", "*.otsx",
                    "*.pptm", "*.xlsm", "*.potm", "*.sldx", "*.docm", "*.dotm", "*.pages", "*.dot", "*.wpd",
                    "*.wps", "*.xlt", "*.wbk", "*.html", "*.htm", "*.epub", "*.md", "*.txt", "*.mdown", "*.markdown"
                }
            },
            { FileCategory.Programming, new List<string>
                {
                    "*.js", "*.ts", "*.jsx", "*.tsx", "*.html", "*.css", "*.scss", "*.less", "*.yaml",
                    "*.json", "*.xml", "*.md", "*.rst", "*.ini", "*.toml", "*.conf", "*.sql", "*.py",
                    "*.rb", "*.java", "*.cpp", "*.h", "*.cs", "*.php", "*.swift", "*.go", "*.rust", "*.pl",
                    "*.bash", "*.sh", "*.zsh", "*.perl", "*.tsv", "*.ejs", "*.pug", "*.twig", "*.mustache",
                    "*.handlebars", "*.erb", "*.hbs", "*.jsp", "*.asp", "*.cfm", "*.json5", "*.yml", "*.xml5",
                    "*.gitignore", "*.gitattributes", "*.editorconfig", "*.eslintrc", "*.babelrc", "*.prettierrc",
                    "*.stylelintrc", "*.npmrc", "*.yarnrc", "*.package.json", "*.package-lock.json", "*.composer.json",
                    "*.webpack.config.js", "*.rollup.config.js", "*.tsconfig.json", "*.babelrc.json", "*.eslint.json",
                    "*.gitmodules", "*.dockerfile", "*.dockerignore", "*.package-lock", "*.vscode/settings.json",
                    "*.gruntfile.js", "*.gulpfile.js", "*.karma.conf.js", "*.config.js", "*.env", "*.env.local",
                    "*.env.production", "*.ansible.cfg", "*.curlrc", "*.vimrc", "*.nano", "*.emacs", "*.zshrc",
                    "*.bashrc", "*.profile", "*.bash_profile", "*.bash_history", "*.zsh_history", "*.crontab",
                    "*.launchd", "*.desktop", "*.plist", "*.my.cnf", "*.httpd.conf", "*.nginx.conf", "*.sshd_config",
                    "*.tmux.conf", "*.ssh_config", "*.nginx.conf", "*.mysql.cnf", "*.dockerfile", "*.nginx.conf",
                    "*.html5", "*.xhtml", "*.jst", "*.json5", "*.jsonc", "*.graphql", "*.graphqls", "*.graphqlschema",
                    "*.swagger.json", "*.swagger.yaml", "*.swagger", "*.gql", "*.graphqlquery", "*.eslintignore",
                    "*.gitconfig", "*.gitmodules", "*.prettierignore", "*.envrc", "*.clang-format", "*.editorconfig",
                    "*.tldr", "*.luarocks", "*.tsconfig", "*.jsconfig", "*.envrc", "*.flake8", "*.pylintrc", "*.tox",
                    "*.mypy.ini", "*.pytest.ini", "*.tox.ini", "*.pytest", "*.makemigrations", "*.yarn.lock", "*.npmrc",
                    "*.yarnrc", "*.bower.json", "*.composer.lock", "*.pipfile", "*.pipfile.lock", "*.hgrc", "*.composer.lock",
                    "*.codecov.yml", "*.styleci.yml", "*.dep.yml", "*.renovate.json", "*.semantic-release", "*.nvmrc",
                    "*.nodemon.json", "*.mocha.opts", "*.serverless.yml", "*.prettierignore", "*.goreleaser.yml", "*.patch",
                    "*.diff", "*.tsv", "*.csv", "*.md", "*.markdown", "*.mdown", "*.markdown", "*.rst", "*.bashrc", "*.zshrc",
                    "*.npmrc", "*.dockerignore", "*.gitconfig", "*.gitmodules", "*.eslintrc", "*.babelrc", "*.prettierrc",
                    "*.stylelintrc", "*.editorconfig", "*.tsconfig", "*.jsx", "*.tsx", "*.ts", "*.js", "*.html", "*.css",
                    "*.scss", "*.less", "*.yaml", "*.json", "*.ini", "*.env", "*.sh", "*.bash", "*.zsh", "*.nginx.conf",
                    "*.httpd.conf", "*.htaccess", "*.tmux.conf", "*.vscode/settings.json", "*.gitattributes", "*.npmignore",
                    "*.package.json", "*.gitignore", "*.editorconfig", "*.bash_profile", "*.vim", "*.emacs", "*.nano"
                }
            },
            { FileCategory.RichText, new List<string>
                {
                    "*.bib", "*.tex", "*.sty", "*.cls", "*.mdown", "*.txtl", "*.txtm", "*.markdown", "*.mdx", "*.rst",
                    "*.xhtml", "*.svg", "*.xml", "*.svgz", "*.patch", "*.diff", "*.gitignore", "*.yarn.lock", "*.npmrc",
                    "*.tsconfig", "*.jsconfig", "*.babelrc", "*.editorconfig", "*.package.json", "*.package-lock.json",
                    "*.composer.json", "*.npmignore", "*.vimrc", "*.nanorc", "*.prettierrc", "*.tsconfig.json", "*.tsconfig",
                    "*.htaccess", "*.dockerfile", "*.dockerignore", "*.gitconfig", "*.gitmodules", "*.vscode/settings.json",
                    "*.ansible.cfg", "*.toml", "*.gitattributes", "*.gitmodules", "*.npmrc", "*.yarnrc", "*.bower.json",
                    "*.composer.lock", "*.tsv", "*.csv", "*.md", "*.markdown", "*.mdown", "*.markdown", "*.rst", "*.bashrc",
                    "*.zshrc", "*.npmrc", "*.dockerignore", "*.gitconfig", "*.gitmodules", "*.eslintrc", "*.babelrc", "*.prettierrc",
                    "*.stylelintrc", "*.editorconfig", "*.tsconfig", "*.jsx", "*.tsx", "*.ts", "*.js", "*.html", "*.css",
                    "*.scss", "*.less", "*.yaml", "*.json", "*.ini", "*.env", "*.sh", "*.bash", "*.zsh", "*.nginx.conf",
                    "*.httpd.conf", "*.htaccess", "*.tmux.conf", "*.vscode/settings.json", "*.gitattributes", "*.npmignore",
                    "*.package.json", "*.gitignore", "*.editorconfig", "*.bash_profile", "*.vim", "*.emacs", "*.nano"
                }
            }
        };
    }
}
