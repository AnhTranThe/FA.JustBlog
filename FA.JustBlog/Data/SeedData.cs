using FA.JustBlog.Common;
using FA.JustBlog.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FA.JustBlog.Data
{
    public static class SeedData
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            // ApplicationDbContext? context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();
            using ApplicationDbContext context = new(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>());

            if (context != null)
            {


                string rootAdminRoleId = ConstantSystem.AdminRoleId;

                string rootUserRoleId = ConstantSystem.UserRoleId;

                string rootAdminId = ConstantSystem.AdminId;

                string rootUserId = ConstantSystem.UserId;

                string TagId_1 = Guid.NewGuid().ToString();
                string TagId_2 = Guid.NewGuid().ToString();
                string TagId_3 = Guid.NewGuid().ToString();
                string CategoryId_1 = Guid.NewGuid().ToString();
                string CategoryId_2 = Guid.NewGuid().ToString();

                string PostId_1 = Guid.NewGuid().ToString();
                string PostId_2 = Guid.NewGuid().ToString();

                if (!context.Roles.Any())
                {
                    List<AppRole> roles = new()
                {
                    new AppRole{Id = rootAdminRoleId, Name = "Admin", NormalizedName = "ADMIN", DisplayName = "Quản trị"},
                    new AppRole{Id = rootUserRoleId, Name = "User", NormalizedName = "USER", DisplayName = "Người dùng"},

                    };
                    foreach (AppRole role in roles)
                    {
                        if (!context.Roles.Any(x => x.Name == role.Name))
                        {
                            _ = await context.Roles.AddAsync(role);

                        }
                    }

                    _ = await context.SaveChangesAsync();
                }

                if (!context.Users.Any())
                {

                    List<AppUser> users = new()
                {
                    new AppUser{ Id = rootAdminId, UserName = "admin", NormalizedUserName = "ADMIN", Email = "admin@gmail.com", NormalizedEmail ="ADMIN@GMAIL.COM", FullName = "FA Admin", FirstName="Admin", LastName="FA", EmailConfirmed = true, PhoneNumberConfirmed = true, TwoFactorEnabled = false, LockoutEnabled = false, AccessFailedCount = 0, SecurityStamp = Guid.NewGuid().ToString() },
                    new AppUser{ Id = rootUserId, UserName="tester", NormalizedUserName = "TESTER",  Email = "tester@gmail.com", NormalizedEmail ="TESTER@GMAIL.COM", FullName = "FA Tester", FirstName="Tester", LastName="FA", EmailConfirmed = true, PhoneNumberConfirmed = true, TwoFactorEnabled = false, LockoutEnabled = false, AccessFailedCount = 0, SecurityStamp = Guid.NewGuid().ToString()   },
                    };

                    foreach (AppUser user in users)
                    {
                        PasswordHasher<AppUser> passwordHasher = new();
                        if (!context.Users.Any(x => x.UserName == user.UserName))
                        {
                            _ = await context.Users.AddAsync(user);
                            user.PasswordHash = passwordHasher.HashPassword(user, "Admin@123");

                        }


                    }


                    _ = await context.SaveChangesAsync();
                }

                if (!context.UserRoles.Any())
                {
                    List<IdentityUserRole<string>> userRoles = new()
                {
                    new IdentityUserRole<string>{RoleId = rootAdminRoleId, UserId = rootAdminId  },
                    new IdentityUserRole<string>{ RoleId = rootUserRoleId, UserId = rootUserId   },
                    };

                    foreach (IdentityUserRole<string> userRole in userRoles)
                    {
                        if (!context.UserRoles.Any(x => x.UserId == userRole.UserId && x.RoleId == userRole.RoleId))
                        {
                            _ = await context.UserRoles.AddAsync(userRole);
                        }
                    }

                    _ = await context.SaveChangesAsync();

                }

                if (!context.Categories.Any())
                {

                    List<Category> categories = new()
                {
                    new Category{Id = CategoryId_1,  Name = ".Net", Slug = "dotnet" , Description="" },
                    new Category{Id = CategoryId_2,  Name = "Java", Slug = "java", Description=""   },
                    new Category{ Name = "React", Slug = "react", Description=""   },

                    };

                    foreach (Category category in categories)
                    {
                        if (!context.Categories.Any(x => x.Name == category.Name))
                        {
                            _ = await context.Categories.AddAsync(category);
                        }
                    }

                    _ = await context.SaveChangesAsync();
                }

                if (!context.Tags.Any())
                {


                    List<Tag> tags = new()
                {
                    new Tag{Id = TagId_1,  Name = "Csharp", Slug = "c-sharp"  },
                    new Tag{Id = TagId_2, Name = "Java", Slug = "java"  },
                    new Tag{Id = TagId_3, Name = "Asp.Net MVC", Slug = "asp-net-mvc"  },

                    };

                    foreach (Tag newTag in tags)
                    {
                        if (!context.Tags.Any(x => x.Name == newTag.Name))
                        {
                            _ = await context.Tags.AddAsync(newTag);
                        }
                    }

                    _ = await context.SaveChangesAsync();
                }

                if (!context.Posts.Any())
                {
                    List<Post> posts = new()
                {
                     new Post
                        {
                            Id = PostId_1,
                            Title = "ASP.NET Core",
                            Content = "ASP.NET Core MVC provides features to build web APIs and web apps: The Model-View-Controller (MVC) pattern helps make your web APIs and web apps testable. Razor Pages is a page-based programming model that makes building web UI easier and more productive. Razor markup provides a productive syntax for Razor Pages and MVC views. Tag Helpers enable server-side code to participate in creating and rendering HTML elements in Razor files. Built-in support for multiple data formats and content negotiation lets your web APIs reach a broad range of clients, including browsers and mobile devices. Model binding automatically maps data from HTTP requests to action method parameters. Model validation automatically performs client-side and server-side validation.",
                            Slug = "asp-net-core",
                            IsActive = true,
                            UserId = rootAdminId,
                            CategoryId = CategoryId_1,




                        },
                        new Post
                        {
                            Id = PostId_2,
                            Title = "Spring Framework",
                            Content = "The Spring Framework is an application framework and inversion of control container for the Java platform.[2] The framework's core features can be used by any Java application, but there are extensions for building web applications on top of the Java EE (Enterprise Edition) platform. The framework does not impose any specific programming model.[citation needed]. The framework has become popular in the Java community as an addition to the Enterprise JavaBeans (EJB) model.[3] The Spring Framework is free and open source software.",
                            Slug = "spring-framework",
                            IsActive = true,
                            UserId = rootAdminId,
                            CategoryId = CategoryId_2,

                        }
                    };
                    foreach (Post post in posts)
                    {
                        if (!context.Posts.Any(x => x.Id == post.Id))
                        {
                            _ = await context.Posts.AddAsync(post);

                        }
                    }

                    _ = await context.SaveChangesAsync();
                }

                if (!context.MapPostTags.Any())
                {

                    List<MapPostTag> mapPostTags = new()
                {
                     new MapPostTag
                        {
                            PostId = PostId_1,
                            TagId = TagId_1


                        },
                        new MapPostTag
                        {
                            PostId = PostId_2,
                            TagId = TagId_2

                        },
                           new MapPostTag
                        {
                            PostId = PostId_1,
                            TagId = TagId_3


                        }
                    };
                    foreach (MapPostTag mapPostTag in mapPostTags)
                    {
                        if (!context.MapPostTags.Any(x => x.PostId == mapPostTag.PostId && x.TagId == mapPostTag.TagId))
                        {
                            _ = await context.MapPostTags.AddAsync(mapPostTag);

                        }
                    }
                    _ = await context.SaveChangesAsync();
                }

                if (!context.Comments.Any())
                {
                    List<Comment> comments = new()
                {
                        new Comment
                        {
                            Text = "Good course",
                            PublishedOn = DateTime.Now.AddDays(-20),
                            UserId = rootUserId,
                            PostId = PostId_1,
                        },
                        new Comment
                        {
                            Text = "That's a very useful course",
                            PublishedOn = DateTime.Now.AddDays(-10),
                            UserId = rootAdminId,
                            PostId = PostId_2,
                        }
                        };

                    foreach (Comment item in comments)
                    {
                        // Check if the corresponding PostId exists in the Posts table
                        if (context.Posts.Any(x => x.Id == item.PostId))
                        {
                            _ = await context.Comments.AddAsync(item);
                        }
                    }

                    _ = await context.SaveChangesAsync();
                }

            }




        }




    }



}


