Travail Pratique 1 complété par Maxime et Simon dans le cadre du cours de développement d'applications graphiques 2 pour l'AEC en développement au Collège Rosemont.

Création d'une interface graphique WPF utilisant Entity Framework (.Net 4.8). L’interface contient toutes les fonctionnalités CRUD permettant d’ajouter, de lire, de modifier, de supprimer et d’effacer une liste d’employés liée à une base de données MS SQL Server.

La liste des employés contient des commandes qui s'affichent à la sélection (double-click) d'un employé. Les commandes, elles, contiennent des clients qui s'affichent dans une deuxième fenêtre à la sélection (double-click) d'une commande.

Respect de toutes les contraintes incluses dans l'énoncé. Ajout additionnel de messages d'erreurs pour toutes les manipulations interdites. Ajout manuel d'une incrémentation pour les IDs à la création de nouveaux employés dans le code behind (C#) puisqu'aucune modification au script de création n'était permise.

Potentielles améliorations:
---------------------------

- Meilleure structure pour le Grid dans le XAML.
- Ajout de RegEx pour toutes les boîtes d'entrées de données.
- Retirer les comboBox de Produits et Catégories puisqu'elles n'ont aucun usage par rapport aux tables affichées (mais c'était nécessaire pour l'énoncé).
- Ajouter les fonctionnalités CRUD pour toutes les autres tables de la base de données.
